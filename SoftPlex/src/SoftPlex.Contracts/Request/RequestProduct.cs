using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Contracts.Request
{
	public class RequestProduct
	{
		private Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		private List<RequestProductVersion> ListRequestProductVersion { get; set; }
	}
}
