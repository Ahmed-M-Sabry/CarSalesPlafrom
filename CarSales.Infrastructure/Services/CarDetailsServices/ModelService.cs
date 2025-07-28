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
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _modelRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Model>> GetAllActiveAsync()
        {
            return await _modelRepository.GetAllActiveAsync();
        }

        public async Task<Model?> GetByIdAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task<Model?> GetByIdWithBrandAsync(int id)
        {
            return await _modelRepository.GetByIdIncludingBrandAsync(id);
        }

        public async Task<Model> CreateAsync(Model model)
        {
            await _modelRepository.AddAsync(model);
            return model;
        }

        public async Task UpdateAsync(Model model)
        {
            await _modelRepository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Model model)
        {
            await _modelRepository.SoftDeleteAsync(model);
        }

        public async Task RestoreAsync(Model model)
        {
            await _modelRepository.RestoreAsync(model);
        }
    }

}
