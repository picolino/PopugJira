using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public GoalsController(GoalTrackerService goalTrackerService)
        {
            this.goalTrackerService = goalTrackerService;
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
        [Route("workflow/{id}/open")]
        public async Task Open([FromQuery] int id)
        {
            await goalTrackerService.OpenGoal(id);
        }

        [HttpPost]
        [Route("workflow/{id}/close")]
        public async Task Close([FromQuery] int id)
        {
            await goalTrackerService.CloseGoal(id);
        }
    }
}