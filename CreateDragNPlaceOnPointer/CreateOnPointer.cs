using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the class that the button should call
/// </summary>
public class CreateOnPointer : MonoBehaviour {
    public float _initialDepth = 0.5f;
    public Camera _cam;
    public Vector3 _mousePos = Vector3.zero;
    public Vector3 _spawnPos = Vector3.zero;

    void Update()
    {
        _mousePos = Input.mousePosition;
        _spawnPos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.nearClipPlane));
    }
    public void CreateObjectOnPointer()
    {
        // this gameobject should be from an object pool
        GameObject go = GameObject.Find("ObjectPoolPlaceholder").GetComponent<PHObjectPool>().GetObject();
        Vector3 offsetDirr = Camera.main.transform.forward;
        if(_cam.orthographic == false)
        {
            offsetDirr = (_spawnPos - _cam.transform.position).normalized;
        }
        Vector3 pos = _spawnPos + offsetDirr * _initialDepth;
        go.transform.position = pos;
        go.SetActive(true);
    }
}
