using Microsoft.EntityFrameworkCore;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.SharedKernel.FilterModels;

namespace PaymentProcessingSystem.Core.Queries;

public class CustomerQueries (PaymentContext context) : ICustomerQueries
{
    public async Task<CommonResponse> GetList(CustomerFilters filters)
    {
        try
        {
  
            var geetCustomerList = await context.Customer.
                Take(filters.pageSize).ToListAsync();
            return CommonResponse.CreateSuccessResponse("Successful", geetCustomerList, "200");

        }
        catch (Exception)
        {

            return CommonResponse.CreateFailedResponse("Failure", "500");
        }

    }
}
