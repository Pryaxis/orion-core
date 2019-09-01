namespace Orion.Tests.Framework.Extensions {
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Orion.Framework.Extensions;
    using Xunit;

    public class TypeExtensionsTests {
        [Fact]
        public void GetAllSubtypes_IsCorrect() {
            var subtypes = typeof(BaseType).GetAllSubtypes().ToList();

            subtypes.Should().HaveCount(2);
            subtypes.Should().Contain(typeof(DerivedType));
            subtypes.Should().Contain(typeof(DerivedType2));
        }
        
        [Fact]
        public void GetGenericTypeMaybe_NonGenericType_IsCorrect() {
            var type = typeof(int).GetGenericTypeMaybe();

            type.Should().Be<int>();
        }
        
        [Fact]
        public void GetGenericTypeMaybe_GenericType_IsCorrect() {
            var type = typeof(List<int>).GetGenericTypeMaybe();

            type.Should().Be(typeof(List<>));
        }



        public class BaseType {
        }

        public class DerivedType : BaseType {

        }

        public class DerivedType2 : BaseType {

        }
    }
}
