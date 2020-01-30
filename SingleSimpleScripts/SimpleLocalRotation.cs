using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLocalRotation : MonoBehaviour
{
    public enum ROTATIONAXIS { X, Y, Z }
    public ROTATIONAXIS _aroundAxis = ROTATIONAXIS.Y;
    public float _RotationSpeed = 90.0f;

    // Update is called once per frame
    void Update()
    {
        float currentRotation = _RotationSpeed * Time.deltaTime;
        if (_aroundAxis == ROTATIONAXIS.Y) {
            this.transform.localEulerAngles = 
                new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + currentRotation, transform.localRotation.eulerAngles.z); }
        else if(_aroundAxis == ROTATIONAXIS.X) {
            this.transform.localEulerAngles = 
                new Vector3(transform.localRotation.eulerAngles.x + currentRotation, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z); }
        else if(_aroundAxis == ROTATIONAXIS.Z) {
            this.transform.localEulerAngles = 
                new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z + currentRotation); }
        
    }
}
