using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Domain.IRepositories.ICarDetailsRepo
{
    public interface ITransmissionTypeRepository : IGenericRepositoryAsync<TransmissionType>
    {
        Task<IEnumerable<TransmissionType>> GetAllActiveAsync();
        Task SoftDeleteAsync(TransmissionType transmissionType);
        Task<TransmissionType> RestoreAsync(TransmissionType item);

    }

}
