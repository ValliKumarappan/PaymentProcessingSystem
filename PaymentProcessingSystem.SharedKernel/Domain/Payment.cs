using PaymentProcessingSystem.SharedKernel.Enums;

namespace PaymentProcessingSystem.SharedKernel.Domain
{
    public class Payment : Entity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public EntityStatusEnum Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Payment()
        {

        }
        public Payment( decimal amount, string currency, PaymentMethod paymentMethod, EntityStatusEnum status, DateTime transactionDate, int customerId)
        {
            Amount = amount;
            Currency = currency;
            PaymentMethod = paymentMethod;
            Status = status;
            TransactionDate = transactionDate;
            CustomerId = customerId;
        }
    }
   
}
