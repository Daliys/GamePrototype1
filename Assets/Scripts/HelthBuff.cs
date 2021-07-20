using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthBuff : MonoBehaviour
{
    [SerializeField]
    private int HelthBuffAddedCount;

    private HelthBuffSpawner helthBuffSpawner;

    public void SetHelthBuffSpawner(HelthBuffSpawner helthBuffSpawner)
    {
        this.helthBuffSpawner = helthBuffSpawner;
    }

    public int GetHelthBuffAddedCount()
    {
        return HelthBuffAddedCount;
    }

    public void DestroyHelthBuff()
    {
        helthBuffSpawner.HelthBuffDestroyed();
        Destroy(gameObject);
    }

  
}
