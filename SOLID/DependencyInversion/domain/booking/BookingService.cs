using SOLID.DependencyInversion.infrastructure;

namespace SOLID.DependencyInversion.domain.booking
{
    public class BookingService
    {
        private readonly ICheckAvailability bookings;

        public BookingService(ICheckAvailability bookings)
        {
            this.bookings = bookings;
        }

        public BookingOutcome Book()
        {
            bool successful = bookings.IsAvailable();
            return new BookingOutcome(successful);
        }
    }
}