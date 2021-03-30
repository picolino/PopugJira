using System;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopugJira.GoalTracker.Application.Commands;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.Application.Queries;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/goals")]
    public class GoalsController : ControllerBase
    {
        private readonly AllGoalsQuery allGoalsQuery;
        private readonly UserGoalsQuery userGoalsQuery;
        private readonly GoalQuery goalQuery;
        private readonly CreateGoalCommand createGoalCommand;
        private readonly UpdateGoalCommand updateGoalCommand;
        private readonly DeleteGoalCommand deleteGoalCommand;
        private readonly CompleteGoalCommand completeGoalCommand;

        public GoalsController(AllGoalsQuery allGoalsQuery,
                               UserGoalsQuery userGoalsQuery,
                               GoalQuery goalQuery,
                               CreateGoalCommand createGoalCommand,
                               UpdateGoalCommand updateGoalCommand,
                               DeleteGoalCommand deleteGoalCommand,
                               CompleteGoalCommand completeGoalCommand)
        {
            this.allGoalsQuery = allGoalsQuery;
            this.userGoalsQuery = userGoalsQuery;
            this.goalQuery = goalQuery;
            this.createGoalCommand = createGoalCommand;
            this.updateGoalCommand = updateGoalCommand;
            this.deleteGoalCommand = deleteGoalCommand;
            this.completeGoalCommand = completeGoalCommand;
        }

        [HttpGet]
        public async Task<Goal[]> GetMineOrAll()
        {
            if (User.IsInRole("admin") || User.IsInRole("manager"))
            {
                return await allGoalsQuery.Query();
            }

            var userId = User.FindFirst(JwtClaimTypes.Subject)?.Value;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return await userGoalsQuery.Query(userId);
            }

            return Array.Empty<Goal>();
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<Goal> Get([FromRoute] string id)
        {
            return await goalQuery.Query(id);
        }

        [HttpPost]
        [Route("new")]
        public async Task Create([FromBody] GoalCreateDto goalCreateDto)
        {
            await createGoalCommand.Execute(goalCreateDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task Update([FromRoute] string id, [FromBody] GoalUpdateDto goalUpdateDto)
        {
            goalUpdateDto.Id = id;
            await updateGoalCommand.Execute(goalUpdateDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            await deleteGoalCommand.Execute(id);
        }

        [HttpPost]
        [Route("workflow/{id}/complete")]
        public async Task Complete([FromRoute] string id)
        {
            await completeGoalCommand.Execute(id);
        }
    }
}