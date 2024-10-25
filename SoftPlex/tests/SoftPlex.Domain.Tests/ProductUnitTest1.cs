using CSharpFunctionalExtensions;
using SoftPlex.Domain.Shared;

namespace SoftPlex.Domain.Tests
{
	public class ProductUnitTest1
	{
		[Fact]
		public void Try_Create_Product_DescNull_ProductReturned()
		{
			//arrange
			

			//act
			Result<Product, ErrorList> productCreated = Product.Create(Guid.NewGuid(), "name", null, null);

			//assert
			//productCreated.IsSuccess
			Assert.True(productCreated.IsSuccess);
		}

		[Fact]
		public void Try_Create_Product_MaxLengthName_ProductReturned()
		{
			//arrange
			string name = new String('a', Product.MAX_NAME_LENGHT);

			//act
			Result<Product, ErrorList> productCreated = Product.Create(Guid.NewGuid(), name, null,null);

			//assert
			//productCreated.IsSuccess
			Assert.True(productCreated.IsSuccess);
		}

		[Fact]
		public void Try_Create_Product_ToLongName_ErrorReturned()
		{
			//arrange
			string name = new String('a', Product.MAX_NAME_LENGHT + 1); 

			//act
			Result<Product, ErrorList> productCreated = Product.Create(Guid.NewGuid(), name, null, null);

			//assert
			//productCreated.IsSuccess
			Assert.True(productCreated.IsFailure);
		}
	}
} 