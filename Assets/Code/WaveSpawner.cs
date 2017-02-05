using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform endPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownTimerText;

    private int waveIndex = 0;

    void Update() {
        if (countdown <= 0) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;

        //waveCountdownTimerText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave() {
        Debug.Log("Spawning wave");
        waveIndex++;
        PlayerStats.Rounds++;

        StartCoroutine(SpawnEnemies(spawnPoint1));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnEnemies(spawnPoint2));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnEnemies(spawnPoint3));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnEnemies(spawnPoint4));
        yield return null;
    }

    IEnumerator SpawnEnemies(Transform position) {
        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy(position);
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }

    void SpawnEnemy(Transform position) {
        Transform enemyTransform = (Transform) Instantiate(enemyPrefab, position.position, position.rotation);
        GameObject enemyGO = enemyTransform.gameObject;
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.SetTarget(endPoint);
    }


}
