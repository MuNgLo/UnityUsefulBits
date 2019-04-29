using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            this.transform.LookAt(target.transform.position);
        }
    }
}
