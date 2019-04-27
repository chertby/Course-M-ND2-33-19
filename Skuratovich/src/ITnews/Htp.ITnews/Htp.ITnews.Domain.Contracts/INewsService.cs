using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts
{
    public interface INewsService
    {
        Task<IEnumerable<NewsViewModel>> GetAllAsync();
        Task<NewsViewModel> GetAsync(Guid id);
        //Task<PaymentModel> AddAsync(CreatePaymentRequest createPaymentRequest);
    }
}
