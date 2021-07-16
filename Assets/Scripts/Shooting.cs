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


   // public GameObject bulletPrefab;
    public Transform bulletSpawnPosition;

    protected virtual void GunShoot(Vector3 bulletDirection)
    {
        GameObject bullet = PoolManager.GetObject("bullet",bulletSpawnPosition.position, Quaternion.identity);

     

        bullet.GetComponent<Bullet>().Setup(bulletDirection, 25, 1f);
        bullet.GetComponent<PoolObject>().ReturnToPool(3);

    }




}
