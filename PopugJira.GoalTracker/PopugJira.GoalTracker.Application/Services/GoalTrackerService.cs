using System.Threading.Tasks;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Services
{
    public class GoalTrackerService
    {
        private readonly IGoalsDataContext goalsDataContext;

        public GoalTrackerService(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
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
            var goal = new Goal(null, createDto.Description, GoalState.Incomplete);
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
    }
}