using UnityEngine;

public class TurretManager : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float rotationSpeed = 10f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform rotatingPart;
    public Transform firePoint;

    void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            } else {
                target = null;
            }

            if (nearestEnemy != null && shortestDistance <= range) {
                target = nearestEnemy.transform;
            }
        }
    }

    void Update() {
        if (target == null) {
            if (useLaser && lineRenderer.enabled) {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }

            return;
        }

        LockOnTarget();

        if (useLaser) {
            Laser();
        } else if (fireCountdown <= 0) {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget() {
        Vector3 directionToEnemy = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        // Improvement?
        //Vector3 rotation = Quaternion.RotateTowards(rotatingPart.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser() {
        //targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        //targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = firePoint.position - target.position;

        Renderer rend = target.gameObject.GetComponent<Renderer>();
        impactEffect.transform.position = target.position + direction.normalized * (rend.bounds.size.x/2f) * 1.1f;
        impactEffect.transform.rotation = Quaternion.LookRotation(direction);
    }

    void Shoot() {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
