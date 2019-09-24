using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetTrigger : MonoBehaviour
{
    private PlayerControls _pControls;
    // Start is called before the first frame update
    void Start()
    {
        _pControls = GameObject.Find("PlayerControls").GetComponent<PlayerControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            _pControls.ResetBall();
        }
    }
}
