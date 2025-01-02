using MediatR;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.SharedKernel.Domain;
using System.Text.RegularExpressions;

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
            if (!IsValid(message.Email))
            {
                return CommonResponse.CreateFailedResponse("Invalid Email Id", "409");
            }

            var customer = new Customer(message.Name, message.Email);

            context.Customer.Add(customer);
            context.SaveChanges();

            return CommonResponse.CreateSuccessResponse($"Customer {customer.Id} updated", customer.Id);
        }
        catch (Exception ex)
        {
            return new CommonResponse { Successful = false, Message = ex.InnerException.Message };
        }
    }
    private bool IsValid(string email)
    {
        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }

}