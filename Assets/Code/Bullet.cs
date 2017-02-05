using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public float bulletSpeed = 70f;
    public float effectRadius = 0f;
    public int damage = 10;
    public int damageRadius = 10;
    public GameObject impactEffect;

    public void Seek(Transform target) {
        this.target = target;
    }

    // Update is called once per frame
    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 targetDirection = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (targetDirection.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }

        transform.Translate(targetDirection.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget() {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        if (effectRadius > 0f) {
            Explode();
            Kill(target);
        } else {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, effectRadius);
        foreach (Collider col in colliders) {
            if (col.tag == "Enemy") {
                DamageRange(col.transform);
            }
        }
    }

    void Damage(Transform enemy) {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null) {
            e.TakeDamage(damage);
        }
    }

    void DamageRange(Transform enemy) {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null) {
            e.TakeDamage(damageRadius);
        }
    }

    void Kill(Transform enemy) {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null) {
            e.TakeDamage(e.health);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }
}
