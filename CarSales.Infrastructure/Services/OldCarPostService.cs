using CarSales.Application.Common;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.Posts;
using CarSales.Domain.IRepositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Services
{
    public class OldCarPostService : IOldCarPostService
    {
        private readonly IOldCarPostRepository _oldCarPostRepository;
        public OldCarPostService(IOldCarPostRepository oldCarPostRepository)
        {
            _oldCarPostRepository = oldCarPostRepository;
        }
        public async Task<Result<OldCarPost>> CreateAsync(OldCarPost post, CancellationToken cancellationToken = default)
        {
            if (post == null)
            {
                return Result<OldCarPost>.Failure("Post cannot be null." , ErrorType.BadRequest);
            }
            await _oldCarPostRepository.AddAsync(post);
            return Result<OldCarPost>.Success(post);
        }

        public async Task<Result<OldCarPost>> GetByIdAsync(string userId , int id, CancellationToken cancellationToken = default)
        {
            var post =  await _oldCarPostRepository.GetByIdAsync(userId , id);
            if (post == null)
            {
                return Result<OldCarPost>.Failure("Post not found.", ErrorType.BadRequest);
            }
            return Result<OldCarPost>.Success(post);
        }

        public async Task<OldCarPost> GetByIdAsync(int id)
        {
            return  await _oldCarPostRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(OldCarPost oldCarPost, CancellationToken cancellationToken = default)
        {
            await _oldCarPostRepository.UpdateAsync(oldCarPost);
        }
    }
}
