﻿using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Accounting.Application.Commands;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.EventBus.Events.UserCud;
using Serviced;

namespace PopugJira.Accounting.Consumers
{
    public class UserCreatedEventConsumer : IConsumeAsync<UserCreatedEventV1>, IScoped
    {
        private readonly CreateAccountCommand createAccountCommand;

        public UserCreatedEventConsumer(CreateAccountCommand createAccountCommand)
        {
            this.createAccountCommand = createAccountCommand;
        }
        
        public async Task ConsumeAsync(UserCreatedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await createAccountCommand.Execute(new CreateAccountDto
                                               {
                                                   Id = message.Id,
                                                   Name = message.Name
                                               });
        }
    }
}