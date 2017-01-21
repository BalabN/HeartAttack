using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {
        if (Input.GetKey("mouse 0")) {
            print("Box Clicked!" + gameObject.transform.position.x);
            GameObject go = (GameObject) Instantiate(Resources.Load("Tower_work_1"));
            print("Box Clicked!" + gameObject.transform.position.x);
            go.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5.6f, gameObject.transform.position.z);
            print("Box Clicked!" + gameObject.transform.position.x);
        }
    }
}
