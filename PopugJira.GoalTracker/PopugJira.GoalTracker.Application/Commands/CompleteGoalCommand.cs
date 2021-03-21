using System;
using System.Threading.Tasks;
using PopugJira.Common;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.BusinessEvents;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CompleteGoalCommand : IScoped
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;
        private readonly IGoalsGetDbOperations goalsGetDbOperations;
        private readonly IGoalsConfigGetDbOperations goalsConfigGetDbOperations;
        private readonly IDateTimeService dateTimeService;
        private readonly IMessageBus messageBus;

        public CompleteGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations,
                                   IGoalsGetDbOperations goalsGetDbOperations,
                                   IGoalsConfigGetDbOperations goalsConfigGetDbOperations,
                                   IDateTimeService dateTimeService,
                                   IMessageBus messageBus)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
            this.goalsGetDbOperations = goalsGetDbOperations;
            this.goalsConfigGetDbOperations = goalsConfigGetDbOperations;
            this.dateTimeService = dateTimeService;
            this.messageBus = messageBus;
        }
        
        public async Task Execute(string id)
        {
            await goalsWriteDbOperations.SetState(GoalState.Complete, id);
            var completeUtcDateTime = dateTimeService.UtcNow;
            var goal = await goalsGetDbOperations.Get(id);
            await messageBus.Publish(new GoalCompletedEventV1
                                     {
                                         Id = id,
                                         AssigneeId = goal.Assignee.Id,
                                         CompletePrice = goal.CompletePrice,
                                         CompleteDateTime = completeUtcDateTime
                                     });
        }
    }
}