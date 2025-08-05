using CarSales.Application.Common;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.Posts;
using CarSales.Domain.IRepositories;
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

    }
}
