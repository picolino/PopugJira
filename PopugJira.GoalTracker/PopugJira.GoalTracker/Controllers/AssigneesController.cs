using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopugJira.GoalTracker.Application.Commands;
using PopugJira.GoalTracker.Application.Dto;

namespace PopugJira.GoalTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/assignees")]
    public class AssigneesController : ControllerBase
    {
        private readonly CreateAssigneeCommand createAssigneeCommand;
        private readonly AssignOpenedGoalsRandomlyCommand assignOpenedGoalsRandomlyCommand;

        public AssigneesController(CreateAssigneeCommand createAssigneeCommand,
                                   AssignOpenedGoalsRandomlyCommand assignOpenedGoalsRandomlyCommand)
        {
            this.createAssigneeCommand = createAssigneeCommand;
            this.assignOpenedGoalsRandomlyCommand = assignOpenedGoalsRandomlyCommand;
        }
        
        [HttpPost]
        [Route("new")]
        public async Task Create([FromBody] AssigneeCreateDto assigneeCreateDto)
        {
            await createAssigneeCommand.Execute(assigneeCreateDto);
        }
        
        [HttpPost]
        [Route("reassign")]
        public async Task Reassign()
        {
            await assignOpenedGoalsRandomlyCommand.Execute();
        }
    }
}