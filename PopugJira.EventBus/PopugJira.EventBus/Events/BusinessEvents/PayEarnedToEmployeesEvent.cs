using System;

namespace PopugJira.EventBus.Events.BusinessEvents
{
    public class PayEarnedToEmployeesEvent
    {
        public string AccountId { get; set; }
        public decimal Earned { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
}