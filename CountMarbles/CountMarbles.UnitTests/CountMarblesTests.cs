using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CountMarbles.UnitTests
{
    [TestClass]
    public class CountMarblesTests
    {
        private CountMarbles count;

        [TestMethod]
        public void EmptyArrayShouldReturnEmptyDictionary()
        {
            //Arrange
            var emptyArray = Array.Empty<string>();
            var colorServiceMock = new Mock<IColorWeightService>();
            var loggerMock = new Mock<ILogger<CountMarbles>>();
            colorServiceMock.Setup(mock => mock.GetColorWeight(It.IsAny<string>())).Returns(0);

            loggerMock.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
            .Callback(new InvocationAction(invocation =>
            {
                //do nothing, you can code here, if you would like verify logs
            }));
            count = new CountMarbles(colorServiceMock.Object, loggerMock.Object);

            //Act
            var result = count.Counter(emptyArray);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            colorServiceMock.Verify(mock => mock.GetColorWeight(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void NullArrayShouldThrowArgmentNullException()
        {
            //Arrange
            string[] nullArray = null;
            var colorServiceMock = new Mock<IColorWeightService>();
            var loggerMock = new Mock<ILogger<CountMarbles>>();
            colorServiceMock.Setup(mock => mock.GetColorWeight(It.IsAny<string>())).Returns(0);

            loggerMock.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
            .Callback(new InvocationAction(invocation =>
            {
                //do nothing, you can code here, if you would like verify logs
            }));
            count = new CountMarbles(colorServiceMock.Object, loggerMock.Object);
            
            //Act
            var act = () => count.Counter(nullArray);

            //Assert
            act.Should().Throw<ArgumentNullException>();
            colorServiceMock.Verify(mock => mock.GetColorWeight(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ColorArrayShouldReturnCorrectCountDictionary()
        {
            //Arrange
            var colurArray = new[] { "red", "blue", "green", "red" };
            var loggerMock = new Mock<ILogger<CountMarbles>>();
            var colorServiceMock = new Mock<IColorWeightService>();
            colorServiceMock.Setup(mock => mock.GetColorWeight("red")).Returns(3);
            colorServiceMock.Setup(mock => mock.GetColorWeight("blue")).Returns(4);
            colorServiceMock.Setup(mock => mock.GetColorWeight("green")).Returns(5);
            loggerMock.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
            .Callback(new InvocationAction(invocation =>
            {
                //do nothing, you can code here, if you would like verify logs
            }));
            count = new CountMarbles(colorServiceMock.Object, loggerMock.Object);

            //Act
            var result = count.Counter(colurArray);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().HaveElementAt(0, new KeyValuePair<string, int>("red", 6));
            result.Should().HaveElementAt(1, new KeyValuePair<string, int>("blue", 4));
            result.Should().HaveElementAt(2, new KeyValuePair<string, int>("green", 5));
            colorServiceMock.Verify(mock => mock.GetColorWeight(It.IsAny<string>()), Times.Exactly(4));
        }

        [TestMethod]
        public void ArrayWithEmptyOrNullValuesShouldIgnoreEmptyOrNullValues()
        {
            //Arrange
            var colorArrayWithEmptyVals = new[] { "red", "blue", null, "green", "" };
            var colorServiceMock = new Mock<IColorWeightService>();
            var loggerMock = new Mock<ILogger<CountMarbles>>();
            colorServiceMock.Setup(mock => mock.GetColorWeight("red")).Returns(3);
            colorServiceMock.Setup(mock => mock.GetColorWeight("blue")).Returns(4);
            colorServiceMock.Setup(mock => mock.GetColorWeight("green")).Returns(5);
            
            loggerMock.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
            .Callback(new InvocationAction(invocation =>
            {
                //do nothing, you can code here, if you would like verify logs
            }));
            count = new CountMarbles(colorServiceMock.Object, loggerMock.Object);

            //Act
            var result = count.Counter(colorArrayWithEmptyVals);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().HaveElementAt(0, new KeyValuePair<string, int>("red", 3));
            result.Should().HaveElementAt(1, new KeyValuePair<string, int>("blue", 4));
            result.Should().HaveElementAt(2, new KeyValuePair<string, int>("green", 5));
            colorServiceMock.Verify(mock => mock.GetColorWeight(It.IsAny<string>()), Times.Exactly(3));
        }
    }
}