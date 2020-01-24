using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyManager : MonoBehaviour
{
    private int currentSupply = 0;
    private int maxSupply = 1;

    public void increaseMaxSupply()
    {
        maxSupply++;
        updateText();
    }

    public void alterCurrentSupply(int i)
    {
        currentSupply += i;
        updateText();
    }

    public int getCurrentSupply()
    {
        return currentSupply;
    }

    public void setCurrentSupply(int i)
    {
        currentSupply = i;
        updateText();
    }

    public int getOverflow()
    {
        return currentSupply - maxSupply;
    }

    private void updateText()
    {
        gameObject.GetComponent<Text>().text = currentSupply + "/" + maxSupply;
        if (currentSupply > maxSupply)
        {
            gameObject.GetComponent<Text>().color = Color.red;
        } else
        {
            gameObject.GetComponent<Text>().color = Color.black;
        }
    }
}
