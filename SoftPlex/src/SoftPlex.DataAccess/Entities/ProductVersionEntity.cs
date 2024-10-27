using SoftPlex.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.DataAccess.Entities
{
	public class ProductVersionEntity
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Length { get; set; }
		public DateTime CreatingDate { get; set; }
	}
}
