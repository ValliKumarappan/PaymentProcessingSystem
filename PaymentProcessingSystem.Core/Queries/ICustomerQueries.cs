using PaymentProcessingSystem.SharedKernel.FilterModels;

namespace PaymentProcessingSystem.Core.Queries;

public interface ICustomerQueries
{
    Task<CommonResponse> GetList(CustomerFilters filters);
}
