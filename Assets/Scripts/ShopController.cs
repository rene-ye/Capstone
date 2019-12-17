using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject menu, board;
    private bool isShowing = false;

    void Start()
    {
        menu.SetActive(isShowing);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
            board.SetActive(!isShowing);
        }
    }
}
