using MediatR;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.SharedKernel.Domain;

namespace PaymentProcessingSystem.Core.Commands;

public class CreateCustomerCommand : IRequest<CommonResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
}
public class CreateCustomerCommandHandler(PaymentContext context) : IRequestHandler<CreateCustomerCommand, CommonResponse>
{
    public async Task<CommonResponse> Handle(CreateCustomerCommand message, CancellationToken cancellationToken)
    {
        try
        {

           var customer = new Customer(message.Name, message.Email);

            context.Customer.Add(customer);
            context.SaveChanges();

            return CommonResponse.CreateSuccessResponse($"Customer {customer.Id} updated", customer.Id);
        }
        catch (Exception ex)
        {
            return new CommonResponse { Successful = false, Message = ex.Message };
        }
    }

}