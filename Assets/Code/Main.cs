using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    private static Main instance;

    [HideInInspector]
    public GameManager GameManager;

    private Main() { }

    public static Main Instance {
        get {
            if (instance == null) {
                instance = new Main();
            }
            return instance;
        }
    }
    private float timeToLoad;
    private bool levelLoaded = false;

    void Awake() {
        InitManagers();
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start() {
        timeToLoad = Time.time + 10;
    }

    private void InitManagers() {
        GameManager = gameObject.AddComponent<GameManager>() as GameManager;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > timeToLoad && !levelLoaded) {
            SceneManager.LoadScene("Level1");
            levelLoaded = true;
        }
    }
}
