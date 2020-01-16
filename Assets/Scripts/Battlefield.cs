using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    const int MAX_X = 12;
    const int MAX_Y = 2;

    // Start is called before the first frame update
    public GameObject ally, enemy;



    private static Dictionary<string,BaseTileHandler> tileMap = new Dictionary<string, BaseTileHandler>();

    private int randomTest = 3;
    void Start()
    {
        foreach (Transform child in ally.transform)
        {
            tileMap.Add(child.name, child.gameObject.GetComponent<BoardTileHandler>());
        }

        foreach (Transform child in enemy.transform)
        {
            tileMap.Add(child.name, child.gameObject.GetComponent<EnemyTileHandler>());
            if(randomTest > 0)
            {
                Axe a = new Axe();
                a.isAlly = false;
                child.gameObject.GetComponent<EnemyTileHandler>().setUnit(a);
                randomTest--;
            }
        }
    }


    public static int[,] generateWeightedGraph()
    {
        int[,] graph = new int[MAX_X, MAX_Y];
        for (int x = 0; x <= MAX_X; x++)
        {
            for (int y = 0; y <= MAX_Y; y++)
            {
                graph[x, y] = tileMap[x + ","+y].getNodeWeight();

            }
        }
        return graph;
    }
    /***********************************************************************************************/
    public static BaseTileHandler getRandomTile()
    {
        List<string> keyList = new List<string>(tileMap.Keys);
        UnityEngine.Random rand = new UnityEngine.Random();
        string randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count)];
        return tileMap[randomKey];
    }
    /***********************************************************************************************/



    public static BaseTileHandler getClosestEnemy(Vector2Int coordinate, bool isAlly)
    {
        Vector2Int v = dijkstra(coordinate, isAlly);
        Debug.Log(v.x + "," + v.y);
        return tileMap[v.x + "," + v.y];
    }

    static Vector2Int dijkstra(Vector2Int start, bool isAlly)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            Vector2Int tile = queue.Dequeue();
            visited.Add(tile);
            BaseTileHandler bTile = tileMap[tile.x + "," + tile.y];
            // Found enemy
            if (bTile.getCurrentUnit() != null && bTile.getCurrentUnit().isAlly != isAlly)
            {
                return tile;
            }
            // Didn't find enemy, get neighbors
            List<Vector2Int> l = findNeighbors(queue, tile.x, tile.y);
            for (int i = 0; i < l.Count; i++)
            {
                Vector2Int v = l[i];
                if (visited.Contains(v))
                {
                    l.RemoveAt(i);
                } else
                {
                    queue.Enqueue(v);
                }
            }
        }
        return new Vector2Int(0,0);
    }

    static List<Vector2Int> findNeighbors(Queue<Vector2Int> queue, int x, int y)
    {
        List<Vector2Int> l = new List<Vector2Int>();
        if (y > 0)
            l.Add(new Vector2Int(x, y - 1));

        if (x % 2 == 0)
        {
            // top and bottom
            if (y < MAX_Y + 1)
                l.Add(new Vector2Int(x, y + 1));
            // right side
            if (x < MAX_X && y <= MAX_Y)
                l.Add(new Vector2Int(x + 1, y));
            if (x < MAX_X && y > 0)
                l.Add(new Vector2Int(x + 1, y - 1));
            // left side
            if (x > 0 && y <= MAX_Y)
                l.Add(new Vector2Int(x - 1, y));
            if (x > 0 && y > 0)
                l.Add(new Vector2Int(x - 1, y - 1));
        } else
        {
            // top and bottom
            if (y < MAX_Y)
                l.Add(new Vector2Int(x, y + 1));
            // right side
            if (x < MAX_X)
                l.Add(new Vector2Int(x + 1, y));
            if (x < MAX_X && y < MAX_Y)
                l.Add(new Vector2Int(x + 1, y + 1));
            // left side
            if (x > 0)
                l.Add(new Vector2Int(x - 1, y));
            if (x > 0 && y < MAX_Y)
                l.Add(new Vector2Int(x - 1, y + 1));

        }
        // Sort by node weight
        l.Sort((a, b) => tileMap[a.x + "," + a.y].getNodeWeight().CompareTo(tileMap[b.x + "," + b.y].getNodeWeight()));
        return l;
    }
}
