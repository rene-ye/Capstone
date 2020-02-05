using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class NeighborTest
    {
        /*
         * First tests if the number of returned tiles is expected and then tests if the distance between the tiles are 1
         */
        private void testNeighbors(Vector2Int[] list, int expectedNeighbors)
        {
            foreach (Vector2Int v in list)
            {
                List<Vector2Int> neighbors = Battlefield.findNeighbors(v.x, v.y);
                Assert.AreEqual(expectedNeighbors, neighbors.Count, v.ToString(), null);
                foreach (Vector2Int vi in neighbors)
                {
                    Assert.AreEqual(1, Battlefield.getDistance(v, vi), v.ToString() + " to " + vi.ToString(), null);
                }
            }
        }

        [Test]
        public void TestTwoNeighbors()
        {
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(0, 3),
                new Vector2Int(12, 0),
                new Vector2Int(12, 3)
            };
            testNeighbors(cases, 2);
        }

        [Test]
        public void TestThreeNeighbors()
        {
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(2, 0),
                new Vector2Int(4, 0),
                new Vector2Int(6, 0),
                new Vector2Int(8, 0),
                new Vector2Int(10, 0),
                new Vector2Int(2, 3),
                new Vector2Int(4, 3),
                new Vector2Int(6, 3),
                new Vector2Int(8, 3),
                new Vector2Int(10, 3)
            };
            testNeighbors(cases, 3);
        }

        [Test]
        public void TestFourNeighbors()
        {
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, 2),
                new Vector2Int(12, 1),
                new Vector2Int(12, 2)
            };
            testNeighbors(cases, 4);
        }

        [Test]
        public void TestFiveNeighbors()
        {
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(3, 0),
                new Vector2Int(5, 0),
                new Vector2Int(7, 0),
                new Vector2Int(9, 0),
                new Vector2Int(11, 0),
                new Vector2Int(1, 2),
                new Vector2Int(3, 2),
                new Vector2Int(5, 2),
                new Vector2Int(7, 2),
                new Vector2Int(9, 2),
                new Vector2Int(11, 2)
            };
            testNeighbors(cases, 5);
        }

        [Test]
        public void TestSixNeighbors()
        {
            Vector2Int[] cases = new Vector2Int[]
            {
                new Vector2Int(1, 1),
                new Vector2Int(3, 1),
                new Vector2Int(5, 1),
                new Vector2Int(7, 1),
                new Vector2Int(9, 1),
                new Vector2Int(11, 1),
                new Vector2Int(2, 1),
                new Vector2Int(2, 2),
                new Vector2Int(4, 1),
                new Vector2Int(4, 2),
                new Vector2Int(6, 1),
                new Vector2Int(6, 2),
                new Vector2Int(8, 1),
                new Vector2Int(8, 2),
                new Vector2Int(10, 1),
                new Vector2Int(10, 2)
            };
            testNeighbors(cases, 6);
        }
    }
}
