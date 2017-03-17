using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace func_rocket
{
    [TestFixture]
    internal class LevelTests
    {
        private static void CheckLevel(Level level)
        {
            var distance = LevelsTask.GetDistance(level.InitialRocket.Location,level.Target);
            Assert.GreaterOrEqual(distance, 450);
            Assert.GreaterOrEqual(550, distance);

            var targetAngle = (level.Target - level.InitialRocket.Location).Angle;
            var initialRocketAngle = level.InitialRocket.Direction;
            Assert.GreaterOrEqual(Math.Abs(targetAngle-initialRocketAngle) %Math.PI, Math.PI/4);
        }

        [Test]
        public void TestLevels()
        {
            foreach (var level in LevelsTask.CreateLevels())
                CheckLevel(level);
        }
    }
}
