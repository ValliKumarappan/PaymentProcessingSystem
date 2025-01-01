using PaymentProcessingSystem.SharedKernel.Domain;
using PaymentProcessingSystem.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingSystem.Core.Commands;

public class UpdatePaymentCommand
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public EntityStatusEnum Status { get; set; }
    public DateTime TransactionDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
public class UpdatePaymentCommandHandler()
{

}