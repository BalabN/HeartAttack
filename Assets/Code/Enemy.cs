using UnityEngine;
using System.Collections;
using HA.PathFinder;

public class Enemy : MonoBehaviour {

    private Transform target;
    public float startSpeed = 10f;
    public float rotationSpeed = 10f;


    [HideInInspector]
    public float speed;

    public float health = 100;
    public int enemyReward = 10;
    Vector3[] path;
    int targetIndex;

    void Start() {
        speed = startSpeed;
    }

    void Update() {
        if (this == null) {
            return;
        }

    }

    public void SetTarget(Transform target) {
        this.target = target;
        PathRequestManager.RequestPath(transform.position, this.target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            StopCoroutine(FollowPath());
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while (true) {
            float distance = Mathf.Abs(Vector3.Distance(transform.position, currentWaypoint));
            if (distance < 15f) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    targetIndex = path.Length - 1;
                    print("Distance = " + distance);
                    if (distance <= 1.5f) {
                        print("Tu sam");
                        EndPath();
                        yield break;
                    }
                }
                currentWaypoint = path[targetIndex];
            }
            LockOnTarget(currentWaypoint);
            //transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * speed;
            // TODO Better add debuff list to go through before setting enemy fields
            speed = startSpeed;
            yield return null;
        }
    }

    void LockOnTarget(Vector3 currentWaypoint) {
        currentWaypoint = new Vector3(currentWaypoint.x, currentWaypoint.y, currentWaypoint.z);
        Vector3 directionToEnemy = (currentWaypoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;

        if (health <= 0) {
            Die();
        }
    }

    public void Slow(float pct) {
        speed = startSpeed * (1f - pct);
    }

    void Die() {
        PlayerStats.Money += enemyReward;

        //GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);

        Destroy(gameObject);
    }

    void EndPath() {
        PlayerStats.Lives -= 1;
        Destroy(gameObject);
    }

    public void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                } else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
