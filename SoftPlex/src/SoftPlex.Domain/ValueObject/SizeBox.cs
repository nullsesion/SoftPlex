using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPlex.Domain.ValueObject
{
	public class SizeBox
	{
		public decimal Width { get; private set; }
		public decimal Height { get; private set; }
		public decimal Length { get; private set; }

		private SizeBox(decimal width, decimal height, decimal length)
		{
			Width = width;
			Height = height;
			Length = length;
		}

		public static Result<SizeBox> Create(decimal width, decimal height, decimal length)
		{
			if (width > 0)
				return Result.Failure<SizeBox>("invalid width");

			if (height > 0)
				return Result.Failure<SizeBox>("invalid height");

			if (length > 0)
				return Result.Failure<SizeBox>("invalid length");

			return Result.Success<SizeBox>(new SizeBox(width, height, length));
		}

		//for ef
		private SizeBox()
		{

		}
	}
}
