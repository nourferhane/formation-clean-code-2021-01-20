namespace SOLID.Liskov
{
    public class Square : IComputeArea
    {

        public int Side { get; set; }

        public int Area => Side * Side;
    }
}