using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthBuffSpawner : MonoBehaviour
{
    
    [SerializeField]
    private GameObject helthBuffPrefab;
    void Start()
    {
        GenerateNewHelthBuff();
    }

    public void HelthBuffDestroyed()
    {
        StartCoroutine(GenerateNewHelthBuffTimer(5));
    }
    public void GenerateNewHelthBuff()
    {
        GameObject gm = Instantiate(helthBuffPrefab, gameObject.transform);
        gm.transform.localPosition = Vector3.up;
        gm.GetComponent<HelthBuff>().SetHelthBuffSpawner(this);
    }


    IEnumerator GenerateNewHelthBuffTimer(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        GenerateNewHelthBuff();
    }
}
