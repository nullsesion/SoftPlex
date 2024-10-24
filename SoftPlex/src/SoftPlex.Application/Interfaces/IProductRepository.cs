﻿using CSharpFunctionalExtensions;
using SoftPlex.Domain;

namespace SoftPlex.Application.Interfaces;

public interface IProductRepository
{
	public Task<Result> InsertOrUpdateProductAsync(Product product, CancellationToken cancellationToken);
	Task<Result<IReadOnlyList<Product>>> GetProductAsync(int page, int pageSize, CancellationToken cancellationToken);
	Task<Result<Product>> GetProductByIdAsync(Guid Id, CancellationToken cancellationToken);

	public Task<Result> RemoveProductByIdAsync(Guid Id, CancellationToken cancellationToken);

	public Task<Result> RemoveProductVersionByIdAsync(Guid Id, CancellationToken cancellationToken);

	public Result<List<FilterEngineDomain>> GetFromFilterEngine(string productNameIn, string productVersionNameIn,
		decimal minSize, decimal maxSize);

}