using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.GetComponent<Player>())
        {
            GameEvents.Instance.RaiseProjectileEvent(
                new ProjectileHitEventArguments()
                {
                    proj = this,
                    ownerID = -1,
                    victimeID = 1,
                    damage = 10
                });
        }
    }

}
