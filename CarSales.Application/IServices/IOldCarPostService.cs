using CarSales.Application.Common;
using CarSales.Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices
{
    public interface IOldCarPostService
    {
        Task<Result<OldCarPost>> CreateAsync(OldCarPost post, CancellationToken cancellationToken = default);
        Task <Result<OldCarPost>> GetByIdAsync(string userId , int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(OldCarPost oldCarPost, CancellationToken cancellationToken = default);
        //Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken = default);
        //Task<bool> ModelExistsAsync(int modelId, CancellationToken cancellationToken = default);
        //Task<bool> FuelTypeExistsAsync(int fuelTypeId, CancellationToken cancellationToken = default);
        //Task<bool> TransmissionTypeExistsAsync(int transmissionTypeId, CancellationToken cancellationToken = default);
    }
}
