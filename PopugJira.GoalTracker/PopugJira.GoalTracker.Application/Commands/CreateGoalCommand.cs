using System;
using System.Threading.Tasks;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.GoalCud;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CreateGoalCommand : IScoped
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;
        private readonly IGoalsConfigGetDbOperations goalsConfigGetDbOperations;
        private readonly IMessageBus messageBus;

        public CreateGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations, 
                                 IGoalsConfigGetDbOperations goalsConfigGetDbOperations,
                                 IMessageBus messageBus)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
            this.goalsConfigGetDbOperations = goalsConfigGetDbOperations;
            this.messageBus = messageBus;
        }

        public async Task Execute(GoalCreateDto goalCreateDto)
        {
            var assignPrice = await goalsConfigGetDbOperations.GetAssignGoalPrice();
            var completePrice = await goalsConfigGetDbOperations.GetCompleteGoalPrice();
            
            var goal = new Goal(Guid.NewGuid().ToString(), goalCreateDto.Title, goalCreateDto.Description, assignPrice, completePrice, GoalState.Incomplete);
            await goalsWriteDbOperations.Create(goal);
            
            await messageBus.Publish(new GoalCreatedEventV1
                                     {
                                         Id = goal.Id,
                                         Title = goal.Title,
                                         Description = goal.Description,
                                         AssignPrice = goal.AssignPrice,
                                         CompletePrice = goal.CompletePrice
                                     });
        }
    }
}