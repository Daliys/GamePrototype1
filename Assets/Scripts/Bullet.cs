using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 bulleDirection;
    private float speed;
    public void Setup(Vector3 direction, float speed)
    {
        bulleDirection = direction;
        this.speed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += bulleDirection * Time.deltaTime * speed;
    }
}
