using System;
using FluentAssertions;
using Orion.Utils;
using Xunit;

namespace Orion.Tests.Utils {
    public class AnnotatableObjectTests {
        [Theory]
        [InlineData(100)]
        public void GetAnnotation_KeyDoesNotExist_ReturnsDefaultValue(int value) {
            var annotatable = new AnnotatableObject();

            annotatable.GetAnnotation("test", value).Should().Be(value);
        }

        [Fact]
        public void GetAnnotation_NullKey_ThrowsArgumentNullException() {
            var annotatable = new AnnotatableObject();
            Action action = () => annotatable.GetAnnotation<int>(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetAnnotation_NullKey_ThrowsArgumentNullException() {
            var annotatable = new AnnotatableObject();
            Action action = () => annotatable.SetAnnotation(null, "");

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(100)]
        public void SetAnnotation_GetAnnotation_IsCorrect(int value) {
            var annotatable = new AnnotatableObject();
            annotatable.SetAnnotation("test", value);

            annotatable.GetAnnotation<int>("test").Should().Be(value);
        }
    }
}
