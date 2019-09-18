using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairMover : MonoBehaviour
{
    public Image _crosshair;
    public RectTransform _panel;
    private bool _bIsInside = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CHMouseEnter()
    {
        Debug.Log("Mouse Enter!");
        _bIsInside = true;
    }
    public void CHMouseExit()
    {
        Debug.Log("Mouse Exit!");
        _bIsInside = false;
    }
    public void CHMouseDrag()
    {
        Debug.Log("Mouse Drag!");
        if (_bIsInside)
        {
            SnapCrossHairToCursor();
        }
    }
    public void CHMouseClick()
    {
        Debug.Log($"Mouse Click! POS: {Input.mousePosition.ToString()}");
        SnapCrossHairToCursor();
    }

    private void SnapCrossHairToCursor()
    {
        _crosshair.transform.position = Input.mousePosition;
    }

    private Vector3 PosTranslation(Vector3 vIn)
    {
        Vector3 vOut = new Vector3();
        vOut.x = vIn.x - _panel.position.x - _panel.
    }
}
