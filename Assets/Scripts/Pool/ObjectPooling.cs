using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private List<PoolObject> poolObjects;
    private GameObject prefab;
    public void Initialization(GameObject prefab, int count)
    {
        poolObjects = new List<PoolObject>();
        this.prefab = prefab;
        for (int i = 0; i < count; i++)
        {
            AddPoolObject();
        }
    }

    private void AddPoolObject()
    {
        GameObject gm = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        gm.name = prefab.name;
        poolObjects.Add(gm.GetComponent<PoolObject>());
        gm.SetActive(false);
    }

    public GameObject GetObject()
    {
        foreach (var item in poolObjects)
        {
            if (!item.isActiveAndEnabled) return item.gameObject;
        }

        AddPoolObject();
        return poolObjects[poolObjects.Count - 1].gameObject;
    }

}
