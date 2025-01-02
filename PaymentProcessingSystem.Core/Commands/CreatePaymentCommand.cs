using MediatR;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.SharedKernel.Domain;
using PaymentProcessingSystem.SharedKernel.Enums;
using PaymentProcessingSystem.ViewModels;
using System.Text.Json;

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
public class CreatePaymentCommandHandler(PaymentContext context) : IRequestHandler<CreatePaymentCommand, CommonResponse>
{
    public async Task<CommonResponse> Handle(CreatePaymentCommand message, CancellationToken cancellationToken)
    {
        try
        {
            if (message.Amount <= 0)
            {
                return CommonResponse.CreateFailedResponse("Amount must be greater than 0", "409");
            }

            var datajson = File.ReadAllText("StaticFiles" + Path.DirectorySeparatorChar + "Currencies.json");
            var countryDtls = JsonSerializer.Deserialize<List<CurrencyList>>(datajson);

            var currencyName = countryDtls?.FirstOrDefault(x => x.code == message.Currency)?.name ?? string.Empty;

            if (string.IsNullOrEmpty(currencyName))
            {
                return CommonResponse.CreateFailedResponse("Invalid Currency", "204");
            }

            var paymentModel = new Payment(message.Amount, message.Currency, message.PaymentMethod,
                message.Status, message.TransactionDate, message.CustomerId);

            context.Payments.Add(paymentModel);
            context.SaveChanges();

            return CommonResponse.CreateSuccessResponse($"Payment {paymentModel.Id} created", paymentModel.Id);
        }
        catch (Exception ex)
        {
            return new CommonResponse { Successful = false, Message = ex.Message };
        }
    }

}