using System;
using System.Linq;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class AssignOpenedGoalsRandomlyCommand : ICommand
    {
        private readonly IGoalsGetDbOperations goalsGetDbOperations;
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;
        private readonly IAssigneesGetDbOperations assigneesGetDbOperations;
        private readonly Random random;

        public AssignOpenedGoalsRandomlyCommand(IGoalsGetDbOperations goalsGetDbOperations,
                                                IGoalsWriteDbOperations goalsWriteDbOperations,
                                                IAssigneesGetDbOperations assigneesGetDbOperations)
        {
            this.goalsGetDbOperations = goalsGetDbOperations;
            this.goalsWriteDbOperations = goalsWriteDbOperations;
            this.assigneesGetDbOperations = assigneesGetDbOperations;

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
                    await goalsWriteDbOperations.SetAssignee(goalId, assigneesIds[random.Next(0, assigneesIds.Length)]);
                }
            }
        }
    }
}