namespace Api.Tests
{
	public class UnitTest1
	{
		[Theory]
		[InlineData(2, 3, 6)]
		[InlineData(5, 4, 20)]
		[InlineData(0, 7, 0)]
		public void TestMultiplication(int a, int b, int expected)
		{
			// Act
			int result = a * b;

			// Assert
			Assert.Equal(expected, result);
		}
	}
}