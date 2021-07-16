using UnityEngine;
using System.Collections;

public class PoolObject: MonoBehaviour
{
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void ReturnToPool(int time)
    {
        StartCoroutine(WaitTimeAndReturnToPool(time));
    }

    IEnumerator WaitTimeAndReturnToPool(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        ReturnToPool();
    }
}
