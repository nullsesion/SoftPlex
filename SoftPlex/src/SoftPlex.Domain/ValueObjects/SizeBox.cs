using CSharpFunctionalExtensions;
using SoftPlex.Domain.Shared;
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
			=> (Width, Height, Length) = (width, height,length);
		
		public static Result<SizeBox, ErrorList> Create(decimal width, decimal height, decimal length)
		{
			ErrorList errorList = new ErrorList();
			//todo: сделать один Result.Failure
			if (width < 0.001m)
				errorList.AddError(new Error("invalid width",ErrorType.Validation, "Width"));

			if (height < 0.001m)
				errorList.AddError(new Error("invalid height", ErrorType.Validation, "Height"));
			
			if (length < 0.001m)
				errorList.AddError(new Error("invalid length", ErrorType.Validation, "Length"));

			if (width == 404m)
				errorList.AddError(new Error("404 invalid width", ErrorType.Validation, "Width"));

			if (height == 404m)
				errorList.AddError(new Error("404 invalid height", ErrorType.Validation, "Height"));

			if (length == 404m)
				errorList.AddError(new Error("404 invalid length", ErrorType.Validation, "Length"));

			if (errorList.IsError)
				return Result.Failure<SizeBox, ErrorList>(errorList);

			return Result.Success<SizeBox, ErrorList>(new SizeBox(width, height, length));
		}
	}
}
