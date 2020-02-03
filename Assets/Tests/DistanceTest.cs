using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class DistanceTest
    {
        private void testDistance(Vector2Int start, Vector2Int end, int distance)
        {
            Assert.AreEqual(distance, Battlefield.getDistance(start, end), start.ToString() + " to " + end.ToString(), null);
        }

        [Test]
        public void TestDistanceHorrizontalEvenCol()
        {
            Vector2Int start = new Vector2Int(0, 0);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(2, 0),
                new Vector2Int(4, 0),
                new Vector2Int(6, 0),
                new Vector2Int(8, 0),
                new Vector2Int(10, 0),
                new Vector2Int(12, 0)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance);
                distance += 2;
            }
        }

        [Test]
        public void TestDistanceHorrizontalOddCol()
        {
            Vector2Int start = new Vector2Int(1, 0);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(3, 0),
                new Vector2Int(5, 0),
                new Vector2Int(7, 0),
                new Vector2Int(9, 0),
                new Vector2Int(11, 0)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance);
                distance += 2;
            }
        }

        [Test]
        public void TestDistanceVerticalEvenCol()
        {
            Vector2Int start = new Vector2Int(0, 0);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(0, 3)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance++);
            }
        }

        [Test]
        public void TestDistanceVerticalOddCol()
        {
            Vector2Int start = new Vector2Int(1, 0);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(1, 1),
                new Vector2Int(1, 2)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance++);
            }
        }

        [Test]
        public void TestDistanceDiagonalTopLeft()
        {
            Vector2Int start = new Vector2Int(0, 0);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
                new Vector2Int(2, 1),
                new Vector2Int(3, 1),
                new Vector2Int(4, 2),
                new Vector2Int(5, 2),
                new Vector2Int(6, 3)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance++);
            }
        }

        [Test]
        public void TestDistanceDiagonalTopRight()
        {
            Vector2Int start = new Vector2Int(12, 0);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(12, 0),
                new Vector2Int(11, 0),
                new Vector2Int(10, 1),
                new Vector2Int(9, 1),
                new Vector2Int(8, 2),
                new Vector2Int(7, 2),
                new Vector2Int(6, 3)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance++);
            }
        }

        [Test]
        public void TestDistanceDiagonalBottomLeft()
        {
            Vector2Int start = new Vector2Int(0, 3);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(0, 3),
                new Vector2Int(1, 2),
                new Vector2Int(2, 2),
                new Vector2Int(3, 1),
                new Vector2Int(4, 1),
                new Vector2Int(5, 0),
                new Vector2Int(6, 0)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance++);
            }
        }

        [Test]
        public void TestDistanceDiagonalBottomRight()
        {
            Vector2Int start = new Vector2Int(12, 3);
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(12, 3),
                new Vector2Int(11, 2),
                new Vector2Int(10, 2),
                new Vector2Int(9, 1),
                new Vector2Int(8, 1),
                new Vector2Int(7, 0),
                new Vector2Int(6, 0)
            };
            int distance = 0;
            foreach (Vector2Int v in cases)
            {
                testDistance(start, v, distance++);
            }
        }
    }
}
