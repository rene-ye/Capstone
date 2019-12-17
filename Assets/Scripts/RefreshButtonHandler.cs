using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButtonHandler : MonoBehaviour
{
    public GameObject playerObj;
    public float AlphaThreshold = 0.1f;
    public Button shopButton1, shopButton2, shopButton3, shopButton4, shopButton5;

    private Player p;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        PlayerInfo pi = playerObj.GetComponent<PlayerInfo>();
        p = pi.getPlayer();
    }

    public void setSprite()
    {
        // shopButton.GetComponent<Image>().sprite = sprites[0];
    }
}
