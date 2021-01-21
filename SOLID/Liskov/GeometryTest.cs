using Xunit;

namespace SOLID.Liskov
{
    public class GeometryTest
    {
        [Fact]
        public void Area_should_be_height_times_width_1()
        {
            area_should_be_height_times_width(new Rectangle { Height = 5, Width = 20 });
        }

        [Fact]
        public void Area_should_be_height_times_width_2()
        {
            area_should_be_height_times_width(new Square { Side = 10 });
        }

        private void area_should_be_height_times_width(IComputeArea computeArea)
        {
            Assert.Equal(100, computeArea.Area);
        }
    }
}