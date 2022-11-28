namespace AdventOfCode22Tests
{
    public class Day0
    {
        [Test]
        public void SystemCheck()
        {
            Console.WriteLine("here (debugging FTW)");
            true.Should().BeTrue(because: "otherwise, this becomes a philosophical problem");
        }
    }
}