using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Application.DtoModels
{
	public record ProductVersionDto(Guid Id
		,Guid ProductId 
		,string Name
		,string? Description
		, decimal Width
		, decimal Height
		, decimal Length);
	
}
