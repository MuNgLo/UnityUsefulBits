using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;
    public ProjectileEvent OnProjectileEvent;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    internal void RaiseProjectileEvent(ProjectileHitEventArguments args)
    {
        Debug.Log("RaiseProjectileEvent raised");
        OnProjectileEvent?.Invoke(args);
    }
}
