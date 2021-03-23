using System.Threading.Tasks;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.GoalCud;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class UpdateGoalCommand : IScoped
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;
        private readonly IMessageBus messageBus;

        public UpdateGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations, 
                                 IMessageBus messageBus)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
            this.messageBus = messageBus;
        }

        public async Task Execute(GoalUpdateDto goalUpdateDto)
        {
            await goalsWriteDbOperations.Update(goalUpdateDto.Id, goalUpdateDto.Title, goalUpdateDto.Description);

            await messageBus.Publish(new GoalUpdatedEventV1
                                     {
                                         Id = goalUpdateDto.Id,
                                         GoalPart = new GoalUpdatedEventV1GoalPart
                                                    {
                                                        Title = goalUpdateDto.Title,
                                                        Description = goalUpdateDto.Description
                                                    }
                                     });
        }
    }
}