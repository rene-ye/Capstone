using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop, board;
    private bool isShowing = true;

    void Start()
    {
        setState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isShowing = !isShowing;
            setState();
        }
    }

    private void setState()
    {
        shop.SetActive(!isShowing);
        board.SetActive(isShowing);
    }
}
