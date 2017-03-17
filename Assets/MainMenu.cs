using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string levelToLoad = "Level1";
    public SceneFader sceneFader;

    public void Play() {
        Debug.Log("Play");
        sceneFader.FadeTo(levelToLoad);
    }
}
