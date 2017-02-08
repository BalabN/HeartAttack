using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

    public Text waveText;

    // Update is called once per frame
    void Update() {
        waveText.text = PlayerStats.Rounds.ToString();
    }
}
