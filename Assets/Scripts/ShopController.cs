using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}
