using CarSales.Application.IServices.ICarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.IRepositories.ICarDetailsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services.CarDetailsServices
{
    public class TransmissionTypeService : ITransmissionTypeService
    {
        private readonly ITransmissionTypeRepository _transmissionTypeRepository;

        public TransmissionTypeService(ITransmissionTypeRepository transmissionTypeRepository)
        {
            _transmissionTypeRepository = transmissionTypeRepository;
        }

        public async Task<TransmissionType> CreateAsync(TransmissionType transmissionType)
        {
            await _transmissionTypeRepository.AddAsync(transmissionType);
            return transmissionType;
        }

        public async Task<IEnumerable<TransmissionType>> GetAllAsync()
        {
            return await _transmissionTypeRepository.GetAllActiveAsync();
        }

        public async Task<TransmissionType> GetByIdAsync(int id)
        {
            var item = await _transmissionTypeRepository.GetByIdAsync(id);

            return item;
        }

        public async Task UpdateAsync(TransmissionType transmissionType)
        {
            await _transmissionTypeRepository.UpdateAsync(transmissionType);
        }

        public async Task DeleteAsync(TransmissionType transmissionType)
        {
            await _transmissionTypeRepository.SoftDeleteAsync(transmissionType);
        }
        public async Task<TransmissionType> RestoreAsync(TransmissionType transmissionType)
        {
            return await _transmissionTypeRepository.RestoreAsync(transmissionType);
        }

    }

}
