using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    private float timeToLoad;
    private bool levelLoaded = false;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start() {
        timeToLoad = Time.time + 10;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > timeToLoad && !levelLoaded) {
            SceneManager.LoadScene("Level1");
            levelLoaded = true;
        }
    }
}
