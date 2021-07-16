using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSetup : MonoBehaviour
{
    [SerializeField]
    private PoolManager.ObjectsToPool[] objectsToPools;
    void Awake()
    {
        PoolManager.Initialization(objectsToPools, gameObject);
    }

}
