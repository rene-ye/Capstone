using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop, bench;
    private bool isShowing = false;

    void Start()
    {
        shop.SetActive(isShowing);
        bench.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isShowing = !isShowing;
            shop.SetActive(isShowing);
        }
    }
}
