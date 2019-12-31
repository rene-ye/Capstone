using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BenchButtonHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float AlphaThreshold = 0.1f;

    private Unit unit = null;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        startPos = this.GetComponent<Image>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUnit(Unit u)
    {
        unit = u;
        this.gameObject.GetComponent<Image>().sprite = UnitSpritePool.getSprite(unit.unit_name);
    }

    public bool isEmpty()
    {
        return (unit == null);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().transform.position = startPos;
    }
}
