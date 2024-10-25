using CSharpFunctionalExtensions;
using SoftPlex.Application.Interfaces;
using SoftPlex.Domain;
using Microsoft.EntityFrameworkCore;
using SoftPlex.DataAccess.Entities;
using SoftPlex.Domain.Shared;

namespace SoftPlex.DataAccess.Repositories
{
	public class ProductRepository : AbstractRepository, IProductRepository
	{

		public ProductRepository(SoftPlexDbContext context) : base(context)
		{
			
		}
		
		public async Task<Result<bool, ErrorList>> InsertOrUpdateProductAsync(Product product, CancellationToken cancellationToken)
		{
			using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
			try
			{
				ProductEntity? productEntityFromDb = _context.Products.FirstOrDefault(x => x.Id == product.Id);
				if (productEntityFromDb is null)
				{
					await _context.Products.AddAsync(
						new ProductEntity()
						{
							Id = product.Id,
							Name = product.Name,
							Description = product.Description,
						}
						, cancellationToken);
				}
				else
				{
					productEntityFromDb.Name = product.Name;
					productEntityFromDb.Description = product.Description;
				}

				if (product.ProductVersions is not null && product.ProductVersions.Count > 0)
				{
					foreach (ProductVersion productVersion in product.ProductVersions)
					{
						ProductVersionEntity? pve = _context
							.ProductVersions
							.FirstOrDefault(x => x.Id == productVersion.Id);

						if (pve is null)
						{
							await _context.ProductVersions.AddAsync(
								new ProductVersionEntity()
								{
									Id = productVersion.Id,
									ProductId = productVersion.ProductId,
									Name = productVersion.Name,
									Description = productVersion.Description,
									Width = productVersion.SizeBox.Width,
									Height = productVersion.SizeBox.Height,
									Length = productVersion.SizeBox.Length,
									CreatingDate = productVersion.CreatingDate.ToUniversalTime()
								}
								, cancellationToken);
						}
						else
						{
							pve.ProductId = productVersion.ProductId;
							pve.Name = productVersion.Name;
							pve.Description = productVersion.Description;
							pve.Width = productVersion.SizeBox.Width;
							pve.Height = productVersion.SizeBox.Height;
							pve.Length = productVersion.SizeBox.Length;
							pve.CreatingDate = productVersion.CreatingDate.ToUniversalTime();
						}
					}
				}
				await _context.SaveChangesAsync(cancellationToken);

				await transaction.CommitAsync(cancellationToken);
			}
			catch (Exception e)
			{
				ErrorList errorList = new ErrorList();
				errorList.AddError(new Error(e.Message, ErrorType.ServerError, null));
				return Result.Failure<bool, ErrorList>(errorList); //tryCreateProduct.Error
			}

			return Result.Success<bool, ErrorList>(true);
		}

		public async Task<Result<bool, ErrorList>> RemoveProductByIdAsync(
			Guid Id
			, CancellationToken cancellationToken)
		{
			try
			{
				await _context
					.Products
					.Where(c => c.Id == Id)
					.ExecuteDeleteAsync();
				//.Remove();
				await _context.SaveChangesAsync(cancellationToken);
				
			}
			catch (Exception e)
			{
				ErrorList errorList = new ErrorList();
				errorList.AddError(new Error(e.Message, ErrorType.ServerError, null));
				return Result.Failure<bool, ErrorList>(errorList); 
			}
			return Result.Success<bool, ErrorList>(true);
		}

		public async Task<Result<bool, ErrorList>> RemoveProductVersionByIdAsync(
			Guid Id
			, CancellationToken cancellationToken)
		{
			try
			{
				await _context
					.ProductVersions
					.Where(c => c.Id == Id)
					.ExecuteDeleteAsync();
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception e)
			{
				ErrorList errorList = new ErrorList();
				errorList.AddError(new Error(e.Message, ErrorType.ServerError, null));
				return Result.Failure<bool, ErrorList>(errorList);
			}
			return Result.Success<bool, ErrorList>(true);
		}

		public async Task<Result<Product, ErrorList>> GetProductByIdAsync(
			Guid Id
			, CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();

			ProductEntity? productEntityFromDb = _context.Products
				.Include(x => x.ProductVersionEntities)
				.AsNoTracking()
				.FirstOrDefault(x => x.Id == Id);
			if (productEntityFromDb == null)
			{
				errorList.AddError(new Error($"Not Found by id {Id}",ErrorType.NotFound,null));
				return Result.Failure<Product, ErrorList>(errorList);
			}
			else
			{

				productEntityFromDb
					    .ProductVersionEntities
					    .ListProductVersionEntityToListProductVersion(out List<ProductVersion> productVersions);
					

				Result<Product, ErrorList> tryCreateProduct = Product.Create(
					productEntityFromDb.Id
					, productEntityFromDb.Name
					, productEntityFromDb.Description
					, productVersions
					);

				if (tryCreateProduct.IsFailure)
				{
					errorList.AddError(new Error($"Server Error in GetProductByIdAsync", ErrorType.ServerError, null));
					return Result.Failure<Product, ErrorList>(errorList); //tryCreateProduct.Error
				}
					
				return Result.Success<Product, ErrorList>(tryCreateProduct.Value);
			} 

		}

		public async Task<Result<IReadOnlyList<Product>, ErrorList>> GetProductAsync(
			  int page
			, int pageSize
			, CancellationToken cancellationToken)
		{
			if (page < 0 && pageSize < 0)
				Result.Failure<IReadOnlyList<Product>>("Invalid parameters");

			List<Product> result = new List<Product>();

			List<ProductEntity> productEntities = _context.Products
				.AsNoTracking()
				.Skip(pageSize * (page - 1 > 0 ? page - 1 : 0))
				.Take(pageSize)
				.Include(x => x.ProductVersionEntities)
				.ToList();//cancellationToken

			List<Product> products = new List<Product>();
			foreach (ProductEntity pe in productEntities)
			{
				//todo: добавить отстрел в логи
				//ошибочные ProductVersion не отображаем 
				if (!pe.ProductVersionEntities.ListProductVersionEntityToListProductVersion(out List<ProductVersion> productVersions))
					continue;

				Result<Product, ErrorList> tryCreateProduct
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

		
		public Result<List<FilterEngineDomain>, ErrorList> GetFromFilterEngine(
			string productNameIn
			, string productVersionNameIn
			, decimal minSize
			, decimal maxSize
		)
		{
			string p1 = productNameIn;
			string p2 = productVersionNameIn;


			//todo: проверить на возможные sqlinjection
			List<FilterEngineEntity> filterEngineEntities = _context.filterEngineEntity
				.FromSqlRaw($"select * from public.filter_engine('{p1}','{p2}',{minSize},{maxSize})", p1, p2)
				.ToList();

			List<FilterEngineDomain> listFilterEngineDomain = new List<FilterEngineDomain>();
			foreach (FilterEngineEntity item in filterEngineEntities)
			{
				listFilterEngineDomain.Add(new FilterEngineDomain()
				{
					Id = item.Id,
					ProductName = item.ProductName,
					ProductVersionName = item.ProductVersionName,
					Width = item.Width,
					Height = item.Height,
					Length = item.Length,
				});
			}
			
			return listFilterEngineDomain;
		}
	}
}
