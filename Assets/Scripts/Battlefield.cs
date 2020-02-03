using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    const int MAX_X = 12;
    const int MAX_Y = 3;

    // Start is called before the first frame update
    public GameObject ally, enemy;
    public Dictionary<string,BaseTileHandler> tileMap = new Dictionary<string, BaseTileHandler>();

    void Start()
    {
        foreach (Transform child in ally.transform)
        {
            tileMap.Add(child.name, child.gameObject.GetComponent<BoardTileHandler>());
        }

        foreach (Transform child in enemy.transform)
        {
            tileMap.Add(child.name, child.gameObject.GetComponent<EnemyTileHandler>());
        }
    }

    public void initAllUnits()
    {
        foreach (BaseTileHandler b in tileMap.Values)
        {
            Unit u = b.getCurrentUnit();
            if (u != null)
            {
                u.resetForCombat();
            }
        }
    }

    // returns the shortest path to the closest enemy
    public List<BaseTileHandler> getClosestEnemy(Vector2Int coordinate, bool isAlly)
    {
        List<Vector2Int> v = dijkstra(coordinate, isAlly);
        if (v != null)
        {
            List<BaseTileHandler> l = new List<BaseTileHandler>();
            for (int i = 0; i < v.Count; i++)
            {
                l.Add(tileMap[v[i].x + "," + v[i].y]);
            }
            return l;
        }
        else
            return null;
    }

    class Node
    {
        public Vector2Int vector;
        public int cost;
        public Node parent;

        public Node(int w, Node p)
        {
            cost = w;
            parent = p;
        }

        public Node(int w, Node p, Vector2Int v)
        {
            cost = w;
            parent = p;
            vector = v;
        }

        public void setVec(Vector2Int v) {
            vector = v;
        }
    }

    List<Vector2Int> dijkstra(Vector2Int start, bool isAlly)
    {
        Node[,] graph = new Node[MAX_X + 1, MAX_Y + 1];
        for (int i = 0; i < MAX_X + 1;i++)
        {
            for (int j = 0; j < MAX_Y + 1; j++)
            {
                graph[i, j] = new Node(Unit.WEIGHT_MAX, null, new Vector2Int(i, j));
            }
        }
        graph[start.x, start.y] = new Node(0, null, start);

        List<Vector2Int> path = new List<Vector2Int>();

        // Not actually a priority queue but we can solve that by sorting
        List<Node> prioQueue = new List<Node>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        // init starting point
        prioQueue.Add(graph[start.x,start.y]);

        while (prioQueue.Count > 0)
        {
            Node current = prioQueue[0];
            prioQueue.RemoveAt(0);
            List<Vector2Int> neighborVectors = findNeighbors(current.vector.x, current.vector.y);
            foreach (Vector2Int v in neighborVectors)
            {
                BaseTileHandler b = tileMap[v.x + "," + v.y];
                if (b.getCurrentUnit() != null && b.getCurrentUnit().isAlly != isAlly)
                {
                    // found enemy
                    path.Add(b.getCoordinate());
                    path.Add(current.vector);
                    while (current.parent != null)
                    {
                        path.Add(current.parent.vector);
                        current = current.parent;
                    }
                    return path;
                } else
                {
                    // check if the weight is smaller than the recorded weight
                    int weight = current.cost + b.getNodeWeight(isAlly);
                    if (graph[v.x,v.y].cost > weight)
                    {
                        graph[v.x, v.y].cost = weight;
                        graph[v.x, v.y].parent = current;
                        if (!prioQueue.Contains(graph[v.x,v.y]))
                        {
                            prioQueue.Add(graph[v.x, v.y]);
                        }
                    }
                }
                // sort the queue by weight after adding all neighbors
                prioQueue.Sort((one, two) => one.cost.CompareTo(two.cost));
                visited.Add(current.vector);
            }
        }
        return null;
    }

    public static List<Vector2Int> findNeighbors(int x, int y)
    {
        List<Vector2Int> l = new List<Vector2Int>();
        if (y > 0)
            l.Add(new Vector2Int(x, y - 1));

        if (x % 2 == 0)
        {
            // top and bottom
            if (y < MAX_Y)
                l.Add(new Vector2Int(x, y + 1));
            // right side
            if (x < MAX_X && y < MAX_Y)
                l.Add(new Vector2Int(x + 1, y));
            if (x < MAX_X && y > 0)
                l.Add(new Vector2Int(x + 1, y - 1));
            // left side
            if (x > 0 && y < MAX_Y)
                l.Add(new Vector2Int(x - 1, y));
            if (x > 0 && y > 0)
                l.Add(new Vector2Int(x - 1, y - 1));
        } else
        {
            // top and bottom
            if (y < MAX_Y - 1)
            {
                l.Add(new Vector2Int(x, y + 1));
            }
            // right side
            if (x < MAX_X)
            {
                l.Add(new Vector2Int(x + 1, y));
                l.Add(new Vector2Int(x + 1, y + 1));
            }
            // left side
            if (x > 0)
            {
                l.Add(new Vector2Int(x - 1, y));
                l.Add(new Vector2Int(x - 1, y + 1));
            }

        }
        return l;
    }

    /*
     * Much more elegant to get the distance in a cube system than to hard code rules for an offset system.
     * Convert the offset coordinates to cube and then just get the distance.
     */
    public static int getDistance(Vector2Int start, Vector2Int end)
    {
        return CubeDistance(OffsetToCube(start), OffsetToCube(end));
    }

    /*
     * Convert the offset coordinate system to a cube coordinate system
     */
    private static Vector3Int OffsetToCube(Vector2Int v)
    {
        int x = v.x;
        int z = v.y - (v.x - (v.x & 1)) / 2;
        int y = -x - z;
        return new Vector3Int(x, y, z);
    }

    /*
     * Calculates the distance between 2 tiles in a cube system
     */
    private static int CubeDistance(Vector3Int start, Vector3Int end)
    {
        return (Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y) + Math.Abs(start.z - end.z)) / 2;
    }
}
