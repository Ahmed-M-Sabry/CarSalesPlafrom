using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices
{
    public interface INewCarPostServices
    {
        Task<Result<NewCarPost>> CreateAsync(NewCarPost post, CancellationToken cancellationToken = default);
        Task<Result<NewCarPost>> GetByIdAsync(string userId, int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(NewCarPost oldCarPost, CancellationToken cancellationToken = default);
    }
}
