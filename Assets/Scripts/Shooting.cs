using UnityEngine;

public class Shooting : MonoBehaviour
{
    private MainCharacterController controller;

    private void Awake()
    {
        controller = GetComponentInParent<MainCharacterController>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = (transform.rotation * Vector3.forward).normalized;
            direction.y = 0;
            GunShoot(direction);
        }
    }

    public Transform bulletSpawnPosition;

    protected virtual void GunShoot(Vector3 bulletDirection)
    {
        GameObject bullet = PoolManager.GetObject("bullet", bulletSpawnPosition.position, Quaternion.identity);
        float damage = controller != null ? controller.GetDamage() : 1f;
        bullet.GetComponent<Bullet>().Setup(bulletDirection, 25, damage);
        bullet.GetComponent<PoolObject>().ReturnToPool(3);
    }
}
