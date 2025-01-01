using PaymentProcessingSystem.SharedKernel.FilterModels;

namespace PaymentProcessingSystem.Core.Queries;

public interface IPaymentQueries
{
    Task<CommonResponse> GetList(PaymentFilters filter);
    Task<CommonResponse> GetListByUsers(string name);
}
