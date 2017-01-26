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

        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy() {
        Transform enemyTransform = (Transform) Instantiate(enemyPrefab, spawnPoint1.position, spawnPoint1.rotation);
        GameObject enemyGO = enemyTransform.gameObject;
        Unit enemy = enemyGO.GetComponent<Unit>();
        enemy.setTarget(endPoint);
    }


}
