using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairMover : MonoBehaviour
{
    public Image _crosshair;
    public RectTransform _panel;
    public float _range = 10.0f;
    private bool _bIsInside = false;

    public void CHMouseEnter()
    {
        //Debug.Log("Mouse Enter!");
        _bIsInside = true;
    }
    public void CHMouseExit()
    {
        //Debug.Log("Mouse Exit!");
        _bIsInside = false;
    }
    public void CHMouseDrag()
    {
        //Debug.Log("Mouse Drag!");
        if (_bIsInside)
        {
            if (InRange())
            {
                SnapCrossHairToCursor();
            }
        }
    }
    public void CHMouseClick()
    {
        //Debug.Log($"Mouse Click! POS: {Input.mousePosition.ToString()}");
        if (InRange())
        {
            SnapCrossHairToCursor();
        }
    }

    private void SnapCrossHairToCursor()
    {
        _crosshair.rectTransform.position = Input.mousePosition;
    }

    private bool InRange()
    {
        Vector3 panelCenter = _panel.position;
        if (Vector3.Distance(panelCenter, Input.mousePosition) < _range)
        {
            Debug.Log("In range!");
            return true;
        }
        Debug.Log("Outside range!");
        return false;
    }
}
