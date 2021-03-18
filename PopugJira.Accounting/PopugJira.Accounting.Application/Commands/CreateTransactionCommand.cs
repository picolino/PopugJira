﻿using System.Threading.Tasks;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.Application.Commands
{
    public class CreateTransactionCommand
    {
        private readonly IAccountsGetDbOperations accountsGetDbOperations;
        private readonly ITransactionsWriteDbOperations transactionsWriteDbOperations;

        public CreateTransactionCommand(IAccountsGetDbOperations accountsGetDbOperations,
                                        ITransactionsWriteDbOperations transactionsWriteDbOperations)
        {
            this.accountsGetDbOperations = accountsGetDbOperations;
            this.transactionsWriteDbOperations = transactionsWriteDbOperations;
        }
        
        public async Task Execute(CreateTransactionDto createDto)
        {
            var account = await accountsGetDbOperations.Get(createDto.AccountId);
            var transaction = new Transaction(null, account, createDto.DateTime, createDto.Debit, createDto.Credit, createDto.Reason);
            await transactionsWriteDbOperations.Create(transaction);
        }
    }
}