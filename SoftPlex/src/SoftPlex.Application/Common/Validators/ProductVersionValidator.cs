﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SoftPlex.Domain;

namespace SoftPlex.Application.Common.Validators
{
	public class ProductVersionValidator:AbstractValidator<ProductVersion>
	{
		public ProductVersionValidator()
		{
		}
	}
}
