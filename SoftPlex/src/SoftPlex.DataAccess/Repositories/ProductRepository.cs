using CSharpFunctionalExtensions;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SoftPlex.DataAccess.Entities;
using System.Reflection.Metadata;

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
			
			using var transaction = _context.Database.BeginTransaction();
			try
			{
				
				
				_context.SaveChanges();
				transaction.Commit();
			}
			catch (Exception e)
			{
				return Result.Failure(e.Message);
			}

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

		public async Task<Result<IReadOnlyList<Product>>> GetProductAsync(
			  int page
			, int pageSize
			, CancellationToken cancellationToken)
		{
			if (page < 0 && pageSize < 0)
				Result.Failure<IReadOnlyList<Product>>("Invalid parameters");

			List<Product> result = new List<Product>();

			List<ProductEntity> productEntities = await _context.Products
				.AsNoTracking()
				.Skip(pageSize * (page - 1 > 0 ? page - 1 : 0))
				.Take(pageSize)
				.Include(x => x.ProductVersionEntities)
				.ToListAsync(cancellationToken);

			List<Product> products = new List<Product>();
			foreach (ProductEntity pe in productEntities)
			{
				//todo: добавить отстрел в логи
				//ошибочные ProductVersion не отображаем 
				if (!pe.ProductVersionEntities.ListProductVersionEntityToListProductVersion(out List<ProductVersion> productVersions))
					continue;

				Result<Product> tryCreateProduct
					= Product.Create(
						pe.Id
						, pe.Name
						, pe.Description
						, productVersions
					);

				if(tryCreateProduct.IsSuccess)
					result.Add(tryCreateProduct.Value);
			}

			return result;
		}
	}
}
