using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpeningHours.Tests
{
    public class TimeTests
    {
        [Test]
        public void Compare_two_times()
        {
            Assert.True(new Time(2, 0) > new Time(1, 0));
            Assert.True(new Time(2, 40) > new Time(2, 30));

            Assert.False(new Time(2, 0) < new Time(1, 0));
            Assert.True(new Time(2, 20) < new Time(2, 30));

            Assert.IsTrue(new Time(2, 30) == new Time(2, 30));
            Assert.IsTrue(new Time(2, 30) != new Time(1, 30));

            Assert.IsTrue(new Time(2, 30) >= new Time(2, 30));
            Assert.IsTrue(new Time(1, 0) <= new Time(1, 30));
        }
    }
}
