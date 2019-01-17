using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class should not really be used. Make a proper object pool instead
/// </summary>
public class PHObjectPool : MonoBehaviour {
    public GameObject placable;

    private void Awake()
    {
        placable.SetActive(false);
    }
    public GameObject GetObject()
    {
        return (GameObject)Instantiate(placable);
    }
}
