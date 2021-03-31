using System;
using Xunit;
using LimestoneDigital.NumberMetamorphoses.Contracts;
using LimestoneDigital.NumberMetamorphoses;

namespace LimestoneDigital.NumberMetamorphoses.Tests
{
    public class ValueTransformerTests
    {
        [Theory]
        [InlineData("1234567","1-7")]
        [InlineData("7654321","1-7")]
        [InlineData("321567","1-3, 5-7")]
        [InlineData("1235","1-3, 5")]
        [InlineData("13476","1, 3-4, 6-7")]
        [InlineData("1236","1-3, 6")]
        [InlineData("612636","1-3, 6")]
        public void Transform_RegularData(string input, string expected)
        {
            // Arrange
            IValueTransformer valueTransformer = new ValueTransformer();

            // Act
            var result = valueTransformer.Transform(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData("word", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        [InlineData("   ", typeof(ArgumentException))]
        [InlineData("12345678", typeof(ArgumentOutOfRangeException))]
        public void Transform_BadData(string input, Type expected)
        {
            // Arrange
            IValueTransformer valueTransformer = new ValueTransformer();

            // Act
            Action result = () => valueTransformer.Transform(input);

            // Assert
            Assert.Throws(expected, result);
        }
    }
}
