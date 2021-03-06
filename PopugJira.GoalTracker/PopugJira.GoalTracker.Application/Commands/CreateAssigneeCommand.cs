﻿using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CreateAssigneeCommand : ICommand
    {
        private readonly IAssigneeWriteDbOperations assigneeWriteDbOperations;

        public CreateAssigneeCommand(IAssigneeWriteDbOperations assigneeWriteDbOperations)
        {
            this.assigneeWriteDbOperations = assigneeWriteDbOperations;
        }
        
        public async Task Execute(AssigneeCreateDto assigneeCreateDto)
        {
            var assignee = new Assignee(null, assigneeCreateDto.UserId, assigneeCreateDto.UserName);
            await assigneeWriteDbOperations.Create(assignee);
        }
    }
}