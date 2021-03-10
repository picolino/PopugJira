using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class AssignOpenedGoalsRandomlyCommand : ICommand
    {
        private readonly IGoalsDataContext goalsDataContext;
        private readonly IAssigneesGetDbOperations assigneesGetDbOperations;
        private readonly Random random;

        public AssignOpenedGoalsRandomlyCommand(IGoalsDataContext goalsDataContext,
                                                IAssigneesGetDbOperations assigneesGetDbOperations)
        {
            this.goalsDataContext = goalsDataContext;
            this.assigneesGetDbOperations = assigneesGetDbOperations;

            random = new Random();
        }
        
        public async Task Execute()
        {
            var incompleteGoalIds = await goalsDataContext.GetIdsByState(GoalState.Incomplete);
            var assigneesIds = await assigneesGetDbOperations.GetAllIds();

            foreach (var goalId in incompleteGoalIds)
            {
                await goalsDataContext.SetAssignee(goalId, assigneesIds[random.Next(0, assigneesIds.Length)]);
            }
        }
    }
}