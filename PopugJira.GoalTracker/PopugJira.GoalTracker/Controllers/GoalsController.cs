using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PopugJira.GoalTracker.Application.Commands;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.Application.Queries;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Controllers
{
    [ApiController]
    [Route("api/v1/goals")]
    public class GoalsController : ControllerBase
    {
        private readonly AllGoalsQuery allGoalsQuery;
        private readonly GoalQuery goalQuery;
        private readonly CreateGoalCommand createGoalCommand;
        private readonly UpdateGoalCommand updateGoalCommand;
        private readonly DeleteGoalCommand deleteGoalCommand;
        private readonly ReopenGoalCommand reopenGoalCommand;
        private readonly CompleteGoalCommand completeGoalCommand;

        public GoalsController(AllGoalsQuery allGoalsQuery,
                               GoalQuery goalQuery,
                               CreateGoalCommand createGoalCommand,
                               UpdateGoalCommand updateGoalCommand,
                               DeleteGoalCommand deleteGoalCommand,
                               ReopenGoalCommand reopenGoalCommand,
                               CompleteGoalCommand completeGoalCommand)
        {
            this.allGoalsQuery = allGoalsQuery;
            this.goalQuery = goalQuery;
            this.createGoalCommand = createGoalCommand;
            this.updateGoalCommand = updateGoalCommand;
            this.deleteGoalCommand = deleteGoalCommand;
            this.reopenGoalCommand = reopenGoalCommand;
            this.completeGoalCommand = completeGoalCommand;
        }

        [HttpGet]
        public async Task<Goal[]> GetAll()
        {
            return await allGoalsQuery.Query();
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<Goal> Get([FromRoute] Guid id)
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
        public async Task Update([FromRoute] Guid id, [FromBody] GoalUpdateDto goalUpdateDto)
        {
            goalUpdateDto.Id = id;
            await updateGoalCommand.Execute(goalUpdateDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await deleteGoalCommand.Execute(id);
        }

        [HttpPost]
        [Route("workflow/{id}/reopen")]
        public async Task Reopen([FromRoute] Guid id)
        {
            await reopenGoalCommand.Execute(id);
        }

        [HttpPost]
        [Route("workflow/{id}/complete")]
        public async Task Complete([FromRoute] Guid id)
        {
            await completeGoalCommand.Execute(id);
        }
    }
}