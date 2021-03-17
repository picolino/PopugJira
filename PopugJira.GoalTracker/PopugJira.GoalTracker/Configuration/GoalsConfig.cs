using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.GoalTracker.Configuration
{
    public class GoalsConfig : IGoalsConfigGetDbOperations, IScoped<IGoalsConfigGetDbOperations>
    {
        private const decimal DefaultAssignPriceFrom = -20;
        private const decimal DefaultAssignPriceTo = -10;
        
        private const decimal DefaultCompletePriceFrom = 20;
        private const decimal DefaultCompletePriceTo = 40;

        private readonly decimal assignPriceFrom;
        private readonly decimal assignPriceTo;
        
        private readonly decimal completePriceFrom;
        private readonly decimal completePriceTo;

        private readonly Random random;

        public GoalsConfig(IConfiguration configuration)
        {
            assignPriceFrom = decimal.TryParse(configuration["GoalsPrices:AssignPriceFrom"], out var configAssignPriceFrom) ? configAssignPriceFrom : DefaultAssignPriceFrom;
            assignPriceTo = decimal.TryParse(configuration["GoalsPrices:AssignPriceTo"], out var configAssignPriceTo) ? configAssignPriceTo : DefaultAssignPriceTo;
            
            completePriceFrom = decimal.TryParse(configuration["GoalsPrices:CompletePriceFrom"], out var configCompletePriceFrom) ? configCompletePriceFrom : DefaultCompletePriceFrom;
            completePriceTo = decimal.TryParse(configuration["GoalsPrices:CompletePriceFrom"], out var configCompletePriceTo) ? configCompletePriceTo : DefaultCompletePriceTo;

            random = new Random();
        }
        
        public Task<decimal> GetAssignGoalPrice()
        {
            var price = NextDecimal(assignPriceFrom, assignPriceTo);
            return Task.FromResult(price);
        }

        public Task<decimal> GetCompleteGoalPrice()
        {
            var price = NextDecimal(completePriceFrom, completePriceTo);
            return Task.FromResult(price);
        }
        
        private decimal NextDecimal(decimal min, decimal max)
        {
            return new decimal(random.NextDouble()) * (max-min) + min;
        }
    }
}