using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public float firerate = 240.0f;
    public float bulletforce = 50.0f;
    public bool canFire = true;
    private float lastShot;
    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) && Time.time > lastShot + 60.0f/firerate && canFire)
        {
            GameObject bullet = (GameObject)GameObject.Instantiate(projectile);
            bullet.transform.position = this.transform.position + Vector3.up * 1.0f;
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletforce, ForceMode2D.Impulse);
            lastShot = Time.time;
        }

    }
}
