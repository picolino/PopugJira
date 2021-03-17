using System;
using System.Linq;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.Common;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.BusinessEvents;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class AssignOpenedGoalsRandomlyCommand : ICommand
    {
        private readonly IGoalsGetDbOperations goalsGetDbOperations;
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;
        private readonly IAssigneesGetDbOperations assigneesGetDbOperations;
        private readonly IGoalsConfigGetDbOperations goalsConfigGetDbOperations;
        private readonly IDateTimeService dateTimeService;
        private readonly IMessageBus messageBus;
        private readonly Random random;

        public AssignOpenedGoalsRandomlyCommand(IGoalsGetDbOperations goalsGetDbOperations,
                                                IGoalsWriteDbOperations goalsWriteDbOperations,
                                                IAssigneesGetDbOperations assigneesGetDbOperations,
                                                IGoalsConfigGetDbOperations goalsConfigGetDbOperations,
                                                IDateTimeService dateTimeService,
                                                IMessageBus messageBus)
        {
            this.goalsGetDbOperations = goalsGetDbOperations;
            this.goalsWriteDbOperations = goalsWriteDbOperations;
            this.assigneesGetDbOperations = assigneesGetDbOperations;
            this.goalsConfigGetDbOperations = goalsConfigGetDbOperations;
            this.dateTimeService = dateTimeService;
            this.messageBus = messageBus;

            random = new Random();
        }

        public async Task Execute()
        {
            var incompleteGoalIds = await goalsGetDbOperations.GetIdsByState(GoalState.Incomplete);
            var assigneesIds = await assigneesGetDbOperations.GetAllIds();

            if (assigneesIds.Any())
            {
                foreach (var goalId in incompleteGoalIds)
                {
                    var selectedAssigneeId = assigneesIds[random.Next(0, assigneesIds.Length)];
                    await goalsWriteDbOperations.SetAssignee(goalId, selectedAssigneeId);
                    var assignUtcDateTime = dateTimeService.UtcNow;
                    var assignPrice = await goalsConfigGetDbOperations.GetAssignGoalPrice();
                    await messageBus.Publish(new GoalAssignedEvent
                                             {
                                                 Id = goalId,
                                                 AssigneeId = selectedAssigneeId,
                                                 AssignPrice = assignPrice,
                                                 AssignDateTime = assignUtcDateTime
                                             });
                }
            }
        }
    }
}