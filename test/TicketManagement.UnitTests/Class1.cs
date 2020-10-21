using NUnit.Framework;

namespace TicketManagement.UnitTests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void SuccsessTest()
        {
            Assert.True(true);
        }

        [Test]
        public void FailedTest()
        {
            Assert.True(false);
        }
    }
}
