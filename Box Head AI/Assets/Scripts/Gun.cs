using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject laserPrefab;
    public Transform firePoint;
    public int damage = 34;
    public float laserLength = 50f;    
    public float knockForce = 200f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.02f;
    public float cooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= cooldown)
        {
            Shoot();
            cooldown = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;
        Vector3 endPoint = firePoint.position + firePoint.forward * laserLength;

        if (Physics.Raycast(ray, out hit, laserLength))
        {
            endPoint = hit.point;

            if (hit.collider.CompareTag("Enemy"))
            {
                Health health = hit.collider.GetComponentInParent<Health>();
                if (health)
                {
                    health.TakeDamage(damage);
                }

                ZoombieController controller = hit.collider.GetComponentInParent<ZoombieController>();
                if (controller)
                {
                    Vector3 direction = (hit.collider.transform.position - firePoint.position).normalized;
                    controller.Knockback(direction, knockForce);
                }
            }

            
        }

        GameObject laser = Instantiate(laserPrefab);
        LineRenderer lr = laser.GetComponent<LineRenderer>();
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, endPoint);
        Destroy(laser, laserDuration);
    }
}
