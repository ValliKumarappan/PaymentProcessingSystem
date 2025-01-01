using MediatR;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.SharedKernel.Domain;
using PaymentProcessingSystem.SharedKernel.Enums;

namespace PaymentProcessingSystem.Core.Commands;

public class UpdatePaymentCommand : IRequest<CommonResponse>

{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public EntityStatusEnum Status { get; set; }
    public DateTime TransactionDate { get; set; }
    public int CustomerId { get; set; }
}
public class UpdatePaymentCommandHandler(PaymentContext context) : IRequestHandler<UpdatePaymentCommand, CommonResponse>
{
    public async Task<CommonResponse> Handle(UpdatePaymentCommand message, CancellationToken cancellationToken)
    {
        try
        {
            var getPayments = context.Payments.FirstOrDefault(x => x.Id == message.Id);

            if (getPayments is not null)
                getPayments.Update(message.Amount, message.Currency,message.PaymentMethod,message.Status,message.TransactionDate,
                    message.CustomerId);

            context.Payments.Update(getPayments);
            context.SaveChanges();

            return CommonResponse.CreateSuccessResponse($"Payment Info {getPayments.Id} updated", getPayments.Id);
        }
        catch (Exception ex)
        {
            return new CommonResponse { Successful = false, Message = ex.Message };
        }
    }

}