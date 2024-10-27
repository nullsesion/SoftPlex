using CSharpFunctionalExtensions;
using SoftPlex.Domain.Shared;
using SoftPlex.Domain.ValueObject;

namespace SoftPlex.Domain.Tests
{
	public class SizeBoxTest1
	{
		[Fact]
		public void Try_Create_SizeBox_1_1_1_SizeBoxReturned()
		{
			//arrange
			

			//act
			Result<SizeBox, ErrorList> sizeBoxTryCreated0 = SizeBox.Create(1,1,1);
			Result<SizeBox, ErrorList> sizeBoxTryCreated1 = SizeBox.Create(10.2m, 10.2m, 10.2m);
			Result<SizeBox, ErrorList> sizeBoxTryCreated2 = SizeBox.Create(100, 5, 3);

			//assert
			Assert.True(sizeBoxTryCreated0.IsSuccess);
			Assert.True(sizeBoxTryCreated1.IsSuccess);
			Assert.True(sizeBoxTryCreated2.IsSuccess);
		}

		[Fact]
		public void Try_Create_SizeBox_Minus_ErrorReturned()
		{
			//arrange


			//act
			Result<SizeBox, ErrorList> sizeBoxTryCreated1 = SizeBox.Create(-1, 10, 10);
			Result<SizeBox, ErrorList> sizeBoxTryCreated2 = SizeBox.Create(10, -1, 10);
			Result<SizeBox, ErrorList> sizeBoxTryCreated3 = SizeBox.Create(10, 10, -1);

			//assert
			Assert.True(sizeBoxTryCreated1.IsFailure);
			Assert.True(sizeBoxTryCreated2.IsFailure);
			Assert.True(sizeBoxTryCreated3.IsFailure);
		}

		[Fact]
		public void Try_Create_SizeBox_Zero_ErrorReturned()
		{
			//arrange


			//act
			Result<SizeBox, ErrorList> sizeBoxTryCreated0 = SizeBox.Create(0, 0, 0);
			Result<SizeBox, ErrorList> sizeBoxTryCreated1 = SizeBox.Create(0, 10, 10);
			Result<SizeBox, ErrorList> sizeBoxTryCreated2 = SizeBox.Create(10, 0, 10);
			Result<SizeBox, ErrorList> sizeBoxTryCreated3 = SizeBox.Create(10, 10, 0);

			//assert
			Assert.True(sizeBoxTryCreated0.IsFailure);
			Assert.True(sizeBoxTryCreated1.IsFailure);
			Assert.True(sizeBoxTryCreated2.IsFailure);
			Assert.True(sizeBoxTryCreated3.IsFailure);
		}
	}
}