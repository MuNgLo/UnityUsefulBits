using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This should be added as a component on the object you want to place with the pointer in world space.
/// Note that mouse 1needs to be pressed and held when this object is enabled.
/// </summary>
public class SpawnAsPlacable : MonoBehaviour {
    public LayerMask _placableSurface;
    public float _currentDepth = 0.5f;
    public bool _fixed = false;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && _fixed == false)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 offsetDirr = Camera.main.transform.forward;
            if (Camera.main.orthographic == false)
            {
                offsetDirr = (worldPos - Camera.main.transform.position).normalized;
            }
            UpdateDepth(worldPos, offsetDirr);
            Vector3 pos = worldPos + offsetDirr * _currentDepth;
            this.gameObject.transform.position = pos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _fixed = true;
        }
    }

    private void UpdateDepth(Vector3 startPos, Vector3 rayDirection)
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(startPos, rayDirection, out hitinfo, Mathf.Infinity, _placableSurface)){
            _currentDepth = hitinfo.distance;
        }
    }
}
