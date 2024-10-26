using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Application.DtoModels
{
	public record RequestProductDTO(
		Guid Id
		, string Name
		, string? Description
		, IEnumerable<ProductVersionDto> ProductVersions
		);

}
