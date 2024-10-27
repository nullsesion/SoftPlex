using CSharpFunctionalExtensions;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.Interfaces;

public interface IProductRepository
{
	public Task<Result<bool, ErrorList>> InsertOrUpdateProductAsync(Product product, CancellationToken cancellationToken);
	Task<Result<IReadOnlyList<Product>, ErrorList>> GetProductAsync(int page, int pageSize, CancellationToken cancellationToken);
	Task<Result<Product, ErrorList>> GetProductByIdAsync(Guid Id, CancellationToken cancellationToken);

	public Task<Result<bool, ErrorList>> RemoveProductByIdAsync(Guid Id, CancellationToken cancellationToken);

	public Task<Result<bool, ErrorList>> RemoveProductVersionByIdAsync(Guid Id, CancellationToken cancellationToken);

	public Result<List<FilterEngineDomain>, ErrorList> GetFromFilterEngine(string productNameIn, string productVersionNameIn,
		decimal minSize, decimal maxSize);

}