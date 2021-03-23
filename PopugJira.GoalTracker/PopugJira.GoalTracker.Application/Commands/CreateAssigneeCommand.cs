using System.Threading.Tasks;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CreateAssigneeCommand : IScoped
    {
        private readonly IAssigneeWriteDbOperations assigneeWriteDbOperations;

        public CreateAssigneeCommand(IAssigneeWriteDbOperations assigneeWriteDbOperations)
        {
            this.assigneeWriteDbOperations = assigneeWriteDbOperations;
        }
        
        public async Task Execute(AssigneeCreateDto assigneeCreateDto)
        {
            var assignee = new Assignee(assigneeCreateDto.Id, assigneeCreateDto.Name);
            await assigneeWriteDbOperations.Create(assignee);
        }
    }
}