using CSharpFunctionalExtensions;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SoftPlex.DataAccess.Repositories
{
	public class ProductRepository : AbstractRepository, IProductRepository
	{

		public ProductRepository(SoftPlexDbContext context) : base(context)
		{
			
		}


		public async Task<Result> InsertOrUpdateProductAsync(Product product, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
			//todo: change to merge
			/*
			Product? productEntityFromDb =
				await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id, cancellationToken);
			if (productEntityFromDb is null)
			{
				await _context.Products.AddAsync(product, cancellationToken);
				return Result.Success();
			}
			else
			{
				productEntityFromDb = product;
			}
			
			return Result.Success();
			*/
		}

		public async Task<Result<IReadOnlyList<Product>>> GetProductAsync(int page, int pageSize,
			CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
			/*
			if (page > 0 && pageSize > 0)
				Result.Failure<IReadOnlyList<Product>>("Invalid parameters");

			List<Product> products = await _context.Products
				.AsNoTracking()
				.Skip(pageSize * (page - 1 > 0 ? page - 1 : 0))
				.Take(pageSize)
				.ToListAsync(cancellationToken);


			return products;
			*/
		}
	}
}
