using UnityEngine;

public static class PoolManager
{
    private static ObjectsToPool[] objectsToPool;

    [System.Serializable]
    public struct ObjectsToPool
    {
        public string name;
        public int count;
        public GameObject prefab;
        [System.NonSerialized]
        public ObjectPooling objectPooling;
    }

    public static void Initialization(ObjectsToPool[] inputObjectToPool, GameObject parent)
    {
        objectsToPool = inputObjectToPool;

        for (int i = 0; i < objectsToPool.Length; i++)
        {
            if(objectsToPool[i].prefab != null)
            {
                GameObject poolObject = new GameObject();
                poolObject.name = "Pool " + objectsToPool[i].name;
                poolObject.transform.parent = parent.transform;
                poolObject.AddComponent<ObjectPooling>();
                objectsToPool[i].objectPooling = poolObject.GetComponent<ObjectPooling>();
                poolObject.GetComponent<ObjectPooling>().Initialization(objectsToPool[i].prefab, objectsToPool[i].count);
            }
        }
    }

    public static GameObject GetObject(string name, Vector3 position, Quaternion quaternion)
    {
        GameObject result = null;

        if (objectsToPool != null)
        {
            foreach (var item in objectsToPool)
            {
                if (string.Compare(item.name, name) == 0)
                {
                    result = item.objectPooling.GetObject();
                    result.transform.position = position;
                    result.transform.rotation = quaternion;
                    result.SetActive(true);
                    return result;
                }
            }
        }

        return result;
    }
}
