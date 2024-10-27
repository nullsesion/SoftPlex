using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Shared.Response
{
	public class ResponseHasErrors
	{
		public bool IsError { get; set; }
		public List<ResponseError> Errors { get; set; }
	}
}
