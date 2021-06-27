using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 bulleDirection;
    private float speed;
    public float damageAtack;

    public void Setup(Vector3 direction, float speed, float damageAtack)
    {
        bulleDirection = direction;
        this.speed = speed;
        this.damageAtack = damageAtack;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += bulleDirection * Time.deltaTime * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
