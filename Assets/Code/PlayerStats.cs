using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public int startMoney = 300;

    void Start() {
        Money = startMoney;
    }
}
