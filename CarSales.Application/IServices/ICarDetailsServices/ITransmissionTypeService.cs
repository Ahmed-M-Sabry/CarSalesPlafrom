using CarSales.Domain.Entities.CarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.IServices.ICarDetailsServices
{
    public interface ITransmissionTypeService
    {
        Task<TransmissionType> GetByIdAsync(int id);
        Task<IEnumerable<TransmissionType>> GetAllAsync();
        Task<TransmissionType> CreateAsync(TransmissionType transmissionType);
        Task UpdateAsync(TransmissionType transmissionType);
        Task DeleteAsync(TransmissionType transmissionType);
        Task<TransmissionType> RestoreAsync(TransmissionType transmissionType);

    }

}
