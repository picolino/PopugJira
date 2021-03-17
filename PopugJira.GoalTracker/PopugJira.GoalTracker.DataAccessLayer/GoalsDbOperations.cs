using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Tools;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.DataAccessLayer.Entities;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class GoalsDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<GoalEntity> Goals => GetTable<GoalEntity>();
        
        public GoalsDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}