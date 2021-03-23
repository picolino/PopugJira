using System;

namespace PopugJira.EventBus.Events.BusinessEvents
{
    public class PayEarnedToEmployeesEventV1
    {
        public string AccountId { get; set; }
        public decimal Earned { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
}