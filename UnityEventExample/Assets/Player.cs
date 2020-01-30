using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    int playerID = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.OnProjectileEvent.AddListener(OnProejtileEvent);
    }

    private void OnProejtileEvent(ProjectileHitEventArguments arg0)
    {
        if(arg0.victimeID == playerID)
        {
            health -= arg0.damage;
            UnityEngine.GameObject.Destroy(arg0.proj.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
