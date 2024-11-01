﻿using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Application.CQRS.Products.Queries
{
	public class GetProducts: IRequest<Result<IReadOnlyList<Product>, ErrorList>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
