using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTile : MonoBehaviour
{
    private Sprite[] sprites;

    public GameObject shopHex;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Tiles/Axe");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // If the space bar is pushed down
        {
            setSprite();
        }
    }

    public void setSprite()
    {
        shopHex.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
