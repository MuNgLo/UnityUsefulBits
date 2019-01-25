using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerinput : NetworkBehaviour {
    public float moveSpeed = 4.0f;
    public float deadspace = 0.25f;
    public Vector3 moveDir = Vector3.zero;
    public Material[] materials;

    void Start()
    {
        if (materials.Length != 2)
        {
            Debug.LogWarning("Player materials aren't setup properly. Needs to be 2. First one used for the local player material.");
        }
        if (hasAuthority)
        {
            Transform mount = this.transform.Find("CameraMount");
            Camera.main.transform.position = mount.position;
            Camera.main.transform.rotation = mount.rotation;
            Camera.main.transform.SetParent(this.transform);
            GetComponent<MeshRenderer>().material = materials[1];
            GetComponent<MeshRenderer>().material = materials[0];
            this.name = "LocalPlayer";
        }
        else
        {
            this.name = "RemotePlayer";
            GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    // Update is called once per frame
    void Update () {
        if (hasAuthority)
        {
            moveDir = Vector3.zero;
            if (Input.GetAxisRaw("Horizontal") > deadspace || Input.GetAxisRaw("Horizontal") < deadspace)
            {
                moveDir.x = Input.GetAxisRaw("Horizontal");
            }

            if (Input.GetAxisRaw("Vertical") > deadspace || Input.GetAxisRaw("Vertical") < deadspace)
            {
                moveDir.z = Input.GetAxisRaw("Vertical");
            }

            moveDir.y = 0.0f;
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
