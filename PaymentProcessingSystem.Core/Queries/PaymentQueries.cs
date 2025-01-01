using Azure.Core;
using MediatR;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using PaymentProcessingSystem.Core.Helpers;
using PaymentProcessingSystem.Infrastructure.Persistence;
using PaymentProcessingSystem.SharedKernel.Domain;
using PaymentProcessingSystem.SharedKernel.FilterModels;
using System.Linq.Expressions;

namespace PaymentProcessingSystem.Core.Queries;

public class PaymentQueries(PaymentContext context) : IPaymentQueries
{
    public async Task<CommonResponse> GetList(PaymentFilters filter)
    {
		try
		{
            var sqlDataRequest = new DataRequest<Payment>
            {
                 Where = x => x.TransactionDate != DateTime.MinValue
            };

            Expression<Func<Payment, bool>> whereAnd;

            if (filter.TransDate != DateTime.MinValue)
            {
                whereAnd = x =>
                    x.TransactionDate >= filter.TransDate;
                sqlDataRequest.Where = ReportExtensions.PredicateBuilder(sqlDataRequest.Where, whereAnd);
            }
            if (filter.Status !=0)
            {
                whereAnd = x =>
                    x.Status == filter.Status;
                sqlDataRequest.Where = ReportExtensions.PredicateBuilder(sqlDataRequest.Where, whereAnd);
            }
            if (filter.PaymentMethod != 0)
            {
                whereAnd = x =>
                    x.PaymentMethod == filter.PaymentMethod;
                sqlDataRequest.Where = ReportExtensions.PredicateBuilder(sqlDataRequest.Where, whereAnd);
            }

            if (!string.IsNullOrEmpty(filter.Currency))
            {
                whereAnd = x =>
                    x.Currency.Contains(filter.Currency);
                sqlDataRequest.Where = ReportExtensions.PredicateBuilder(sqlDataRequest.Where, whereAnd);
            }

            var getPaymentList = await context.Payments.Where(sqlDataRequest.Where).
                Take(filter.pageSize).ToListAsync();
            return CommonResponse.CreateSuccessResponse("Successful", getPaymentList, "200");

        }
		catch (Exception)
		{

            return CommonResponse.CreateFailedResponse("Failure", "500");
        }
    
    }

    public async Task<CommonResponse> GetListByUsers(string name)
    {
        try
        {
 
            var getPaymentList = await context.Payments.Include(x=>x.Customer)
                .Where(x=>x.Customer.Name.Contains(name))
                .Take(20).ToListAsync();

            return CommonResponse.CreateSuccessResponse("Successful", getPaymentList, "200");

        }
        catch (Exception)
        {

            return CommonResponse.CreateFailedResponse("Failure", "500");
        }
    }
}

