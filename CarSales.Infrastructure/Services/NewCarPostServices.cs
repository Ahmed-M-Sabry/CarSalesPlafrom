using CarSales.Application.Common;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.Posts;
using CarSales.Domain.IRepositories;
using CarSales.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services
{
    public class NewCarPostServices : INewCarPostServices
    {
        private readonly INewCarPostRepository _newCarPostRepository;
        public NewCarPostServices(INewCarPostRepository newCarPostRepository)
        {
            _newCarPostRepository = newCarPostRepository;
        }
        public async Task<Result<NewCarPost>> CreateAsync(NewCarPost post, CancellationToken cancellationToken = default)
        {
            if (post == null)
            {
                return Result<NewCarPost>.Failure("Post cannot be null.", ErrorType.BadRequest);
            }
            await _newCarPostRepository.AddAsync(post);
            return Result<NewCarPost>.Success(post);
        }

        public async Task<Result<NewCarPost>> GetByIdAsync(string userId, int id, CancellationToken cancellationToken = default)
        {
            var post = await _newCarPostRepository.GetByIdAsync(userId, id);
            if (post == null)
            {
                return Result<NewCarPost>.Failure("Post not found.", ErrorType.BadRequest);
            }
            return Result<NewCarPost>.Success(post);
        }

        public async Task<NewCarPost> GetByIdAsync(int id)
        {
            return await _newCarPostRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(NewCarPost oldCarPost, CancellationToken cancellationToken = default)
        {
            await _newCarPostRepository.UpdateAsync(oldCarPost);
        }
    }
}
