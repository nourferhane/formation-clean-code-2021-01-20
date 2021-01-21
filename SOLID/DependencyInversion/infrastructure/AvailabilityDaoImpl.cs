using SOLID.DependencyInversion.domain.booking;

namespace SOLID.DependencyInversion.infrastructure
{
    public class AvailabilityDaoImpl : ICheckAvailability
    {

        public virtual bool IsAvailable()
        {
            //En realite il y aurait une dependance vers une base de donn�esS...
            return true;
        }

    }
}
