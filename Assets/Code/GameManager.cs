using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static bool GameIsOver = false;

    //public GameObject gameOverUI;

    void Start() {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update() {
        if (GameIsOver) {
            return;
        }

        if (Input.GetKeyDown("e")) {
            EndGame();
        }

        if (PlayerStats.Lives <= 0) {
            EndGame();
        }
    }

    public void EndGame() {
        GameIsOver = true;
        Debug.Log("Game is over");
        //gameOverUI.SetActive(true);
    }
}
