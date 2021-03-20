using System;

namespace PopugJira.Analytics.Application.Dtos
{
    public class PaymentInfoDto
    {
        public decimal Earned { get; init; }
        public DateTime PaymentDateTime { get; init; }
    }
}