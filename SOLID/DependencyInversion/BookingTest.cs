using Moq;
using SOLID.DependencyInversion.domain.booking;
using SOLID.DependencyInversion.infrastructure;
using Xunit;

namespace SOLID.DependencyInversion
{
    public class BookingTest
    {
        private Mock<ICheckAvailability> availabilities;
        private BookingService booking;

        public BookingTest()
        {
            availabilities = new Mock<ICheckAvailability>();
            booking = new BookingService(availabilities.Object);
        }

        [Fact]
        public void should_book_when_available()
        {
            availabilities.Setup(a => a.IsAvailable()).Returns(true);
            BookingOutcome outcome = booking.Book();

            Assert.True(outcome.IsSuccessful());
        }

        [Fact]
        public void should_not_book_when_not_available()
        {
            availabilities.Setup(a => a.IsAvailable()).Returns(false);

            BookingOutcome outcome = booking.Book();

            Assert.False(outcome.IsSuccessful());
        }
    }
}