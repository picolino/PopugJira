using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.Common;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.BusinessEvents;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CompleteGoalCommand : ICommand
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
            var goalCompletePrice = await goalsConfigGetDbOperations.GetCompleteGoalPrice();
            await messageBus.Publish(new GoalCompletedEvent
                                     {
                                         Id = id,
                                         AssigneeId = goal.Assignee.Id,
                                         CompletePrice = goalCompletePrice,
                                         CompleteDateTime = completeUtcDateTime
                                     });
        }
    }
}