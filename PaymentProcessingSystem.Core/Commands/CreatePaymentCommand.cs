using MediatR;
using Microsoft.Extensions.Logging;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.SharedKernel.Domain;
using PaymentProcessingSystem.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingSystem.Core.Commands;

public class CreatePaymentCommand : IRequest<CommonResponse>
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public EntityStatusEnum Status { get; set; }
    public DateTime TransactionDate { get; set; }
    public int CustomerId { get; set; }
}
public class CreatePaymentCommandHandler(PaymentContext context) : IRequestHandler<CreatePaymentCommand,CommonResponse>
{


    public async Task<CommonResponse> Handle(CreatePaymentCommand message, CancellationToken cancellationToken)
    {
        try
        {

            var policyModel = new Payment(message.Amount, message.Currency, message.PaymentMethod,
                message.Status, message.TransactionDate, message.CustomerId);

            context.Payments.Add(policyModel);
            context.SaveChanges();

            //if (!result)
            //    return CommonResponse.CreateFailedResponse("Some error occured while saving policy, try again later");

           return CommonResponse.CreateSuccessResponse($"Policy Id {policyModel.Id} Updated Added to Database",
                policyModel.Id);
        }
        catch (Exception ex)
        {
            return new CommonResponse { Successful = false, Message = ex.Message };
        }
    }

}