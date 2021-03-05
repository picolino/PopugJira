using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PopugJira.GoalTracker.Application.Commands;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.Application.Services;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Controllers
{
    [ApiController]
    [Route("api/v1/goals")]
    public class GoalsController : ControllerBase
    {
        private readonly GoalTrackerService goalTrackerService;
        private readonly ReopenGoalCommand reopenGoalCommand;
        private readonly CompleteGoalCommand completeGoalCommand;

        public GoalsController(GoalTrackerService goalTrackerService,
                               ReopenGoalCommand reopenGoalCommand,
                               CompleteGoalCommand completeGoalCommand)
        {
            this.goalTrackerService = goalTrackerService;
            this.reopenGoalCommand = reopenGoalCommand;
            this.completeGoalCommand = completeGoalCommand;
        }

        [HttpGet]
        public async Task<Goal[]> GetAll()
        {
            return await goalTrackerService.GetAllGoals();
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<Goal> Get([FromRoute] int id)
        {
            return await goalTrackerService.GetGoal(id);
        }

        [HttpPost]
        [Route("new")]
        public async Task Create([FromBody] GoalCreateDto goalCreateDto)
        {
            await goalTrackerService.CreateGoal(goalCreateDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] GoalUpdateDto goalUpdateDto)
        {
            goalUpdateDto.Id = id;
            await goalTrackerService.UpdateGoal(goalUpdateDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await goalTrackerService.DeleteGoal(id);
        }

        [HttpPost]
        [Route("workflow/{id}/reopen")]
        public async Task Reopen([FromRoute] int id)
        {
            await reopenGoalCommand.Execute(id);
        }

        [HttpPost]
        [Route("workflow/{id}/complete")]
        public async Task Complete([FromRoute] int id)
        {
            await completeGoalCommand.Execute(id);
        }
    }
}