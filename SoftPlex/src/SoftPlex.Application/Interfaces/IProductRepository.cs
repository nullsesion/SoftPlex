using CSharpFunctionalExtensions;
using SoftPlex.Domain;

namespace SoftPlex.Application.Interfaces;

public interface IProductRepository
{
	public Task<Result> InsertOrUpdateProductAsync(Product product, CancellationToken cancellationToken);
	Task<Result<IReadOnlyList<Product>>> GetProductAsync(int page, int pageSize, CancellationToken cancellationToken);
	public Task SaveAsync();
}