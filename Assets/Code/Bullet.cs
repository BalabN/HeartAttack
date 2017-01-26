using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public float bulletSpeed = 70f;
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
    }

    private void HitTarget() {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        PlayerStats.Money += 10;
        Destroy(effectInstance, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);
    }

}
