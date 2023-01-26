using FluentAssertions;

namespace CountMarbles.UnitTests
{
    [TestClass]
    public class CountMarblesTests
    {
        private readonly CountMarbles count = new();


        [TestMethod]
        public void EmptyArrayShouldReturnEmptyDictionary()
        {
            //Arrange
            var emptyArray = Array.Empty<string>();

            //Act
            var result = count.Counter(emptyArray);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void NullArrayShouldThrowArgmentNullException()
        {
            //Arrange
            string[] nullArray = null;

            //Act
            var act = () => count.Counter(nullArray);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }


        [TestMethod]
        public void ColorArrayShouldReturnCorrectCountDictionary()
        {
            //Arrange
            var colurArray = new[] { "red", "blue", "green", "red" };

            //Act
            var result = count.Counter(colurArray);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().HaveElementAt(0, new KeyValuePair<string, int>("red", 2));
            result.Should().HaveElementAt(1, new KeyValuePair<string, int>("blue", 1));
            result.Should().HaveElementAt(2, new KeyValuePair<string, int>("green", 1));
        }


        [TestMethod]
        public void ArrayWithEmptyOrNullValuesShouldIgnoreEmptyOrNullValues()
        {
            //Arrange
            var colorArrayWithEmptyVals = new[] { "red", "blue", null, "green", "" };

            //Act
            var result = count.Counter(colorArrayWithEmptyVals);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().HaveElementAt(0, new KeyValuePair<string, int>("red", 1));
            result.Should().HaveElementAt(1, new KeyValuePair<string, int>("blue", 1));
            result.Should().HaveElementAt(2, new KeyValuePair<string, int>("green", 1));
        }
    }
}