using System.Threading.Tasks;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Services
{
    public class GoalTrackerService
    {
        private readonly IGoalsDataContext goalsDataContext;
        private readonly IGoalStatesDataContext goalStatesDataContext;

        public GoalTrackerService(IGoalsDataContext goalsDataContext, 
                                  IGoalStatesDataContext goalStatesDataContext)
        {
            this.goalsDataContext = goalsDataContext;
            this.goalStatesDataContext = goalStatesDataContext;
        }

        public async Task<Goal[]> GetAllGoals()
        {
            return await goalsDataContext.GetAll();
        }
        
        public async Task<Goal> GetGoal(int id)
        {
            return await goalsDataContext.Get(id);
        }
        
        public async Task CreateGoal(GoalCreateDto createDto)
        {
            var openState = await goalStatesDataContext.GetOpenState();
            var goal = new Goal(null, createDto.Description, openState);
            await goalsDataContext.Create(goal);
        }

        public async Task UpdateGoal(GoalUpdateDto goalUpdateDto)
        {
            await goalsDataContext.Update(goalUpdateDto.Id, goalUpdateDto.Description);
        }

        public async Task DeleteGoal(int id)
        {
            await goalsDataContext.Delete(id);
        }

        public async Task OpenGoal(int id)
        {
            var openState = await goalStatesDataContext.GetOpenState();
            await goalsDataContext.SetState(id, openState.Id);
        }

        public async Task CloseGoal(int id)
        {
            var closedState = await goalStatesDataContext.GetClosedState();
            await goalsDataContext.SetState(id, closedState.Id);
        }
    }
}