using CSharpFunctionalExtensions;
using SoftPlex.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Domain.Tests
{
	public class ProductVersionTest1
	{
		[Fact]
		public void Try_Create_ProductVersion_blablabla_SizeBoxReturned()
		{
			//arrange
			Result<SizeBox, ErrorList> sizeBoxTryCreated = SizeBox.Create(10.2m, 10.2m, 10.2m);
			if (sizeBoxTryCreated.IsFailure)
			{
				Assert.False(sizeBoxTryCreated.IsFailure);
				return;
			}

			//act
			//d09e0847-49a2-4893-825c-d786cec086c9	9382c4ec-4642-40c8-950d-c302ecfcd51c	Фрезерный		100	200	100	2024-10-22 01:19:15.924 +0300
			Result<ProductVersion, ErrorList> pve = ProductVersion.Create(
				Guid.NewGuid()
				, Guid.NewGuid()
				, "Фрезерный"
				, "Фрезерный станк по дереву"
				, sizeBoxTryCreated.Value
				, DateTime.Now
			);


			//assert
			Assert.True(pve.IsSuccess);

		}
	}
}
