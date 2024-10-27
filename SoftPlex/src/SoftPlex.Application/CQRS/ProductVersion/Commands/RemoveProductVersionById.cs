using CSharpFunctionalExtensions;
using MediatR;
using SoftPlex.Domain;
using SoftPlex.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Application.CQRS.ProductVersion.Commands
{
	public class RemoveProductVersionById: IRequest<Result<bool, ErrorList>>
	{
		public Guid Id { get; set; }
	}
}
