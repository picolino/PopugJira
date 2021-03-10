using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PopugJira.GoalTracker.Application.Commands;

namespace PopugJira.GoalTracker.Controllers
{
    [ApiController]
    [Route("api/v1/assignees")]
    public class AssigneesController : ControllerBase
    {
        private readonly AssignOpenedGoalsRandomlyCommand assignOpenedGoalsRandomlyCommand;

        public AssigneesController(AssignOpenedGoalsRandomlyCommand assignOpenedGoalsRandomlyCommand)
        {
            this.assignOpenedGoalsRandomlyCommand = assignOpenedGoalsRandomlyCommand;
        }
        
        [HttpPost]
        [Route("reassign")]
        public async Task Reassign()
        {
            await assignOpenedGoalsRandomlyCommand.Execute();
        }
    }
}