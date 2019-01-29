using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour {
    public float _TTL = 10.0f;
	
	// Update is called once per frame
	void Update () {
        _TTL -= Time.deltaTime;
        if(_TTL <= 0.0f)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
