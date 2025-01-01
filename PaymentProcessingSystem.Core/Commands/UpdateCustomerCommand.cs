using MediatR;
using PaymentProcessingSystem.Infrastructure.Persistence;

namespace PaymentProcessingSystem.Core.Commands;

public class UpdateCustomerCommand : IRequest<CommonResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
public class UpdateCustomerCommandHandler(PaymentContext context) : IRequestHandler<UpdateCustomerCommand, CommonResponse>
{
    public async Task<CommonResponse> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
    {
        try
        {
            var getCustomer = context.Customer.FirstOrDefault(x => x.Id == message.Id);

            if (getCustomer is not null)
                getCustomer.Update(message.Name, message.Email);

            context.Customer.Update(getCustomer);
            context.SaveChanges();

            return CommonResponse.CreateSuccessResponse($"Customer {getCustomer.Id} updated", getCustomer.Id);
        }
        catch (Exception ex)
        {
            return new CommonResponse { Successful = false, Message = ex.Message };
        }
    }

}
