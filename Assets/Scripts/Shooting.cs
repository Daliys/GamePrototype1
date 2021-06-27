using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 2 ways of shooting (1 to mouse posion \ 2 to formard by rotation)

            // Vector3 direction = (Utils.mouseWorldPosition - bulletSpawnPosition.position).normalized;
            //  direction.y = 0;

            Vector3 direction = (transform.rotation * Vector3.forward).normalized;
            direction.y = 0;

            GunShoot(direction);
        }
    }


    public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;

    protected virtual void GunShoot(Vector3 bulletDirection)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity, null);

     

        bullet.GetComponent<Bullet>().Setup(bulletDirection, 25, 1f);
        Destroy(bullet, 3);

    }




}
