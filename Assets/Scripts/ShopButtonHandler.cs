using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    private Sprite[] sprites;
    public float AlphaThreshold = 0.1f;
    public Button shopButton;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Tiles/Axe");
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }

    public void setSprite()
    {
        shopButton.GetComponent<Image>().sprite = sprites[0];
    }
}
