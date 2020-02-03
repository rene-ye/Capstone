using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BenchManager : MonoBehaviour
{
    BenchButtonHandler[] bench = null;
    public GameObject one, two, three, four, five, six, seven, eight, nine, ten;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] benchButtons = new GameObject[] { one, two, three, four, five, six, seven, eight, nine, ten };
        bench = new BenchButtonHandler[benchButtons.Length];
        for (int i = 0; i < bench.Length; i++)
        {
            bench[i] = benchButtons[i].GetComponent<BenchButtonHandler>();
        }
    }

    public bool addToBench(Unit u)
    {
        for (int i = 0; i < bench.Length; i++)
        {
            if (bench[i].isEmpty())
            {
                bench[i].setUnit(u);
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
