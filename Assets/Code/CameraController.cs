﻿using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Camera")]
    public bool doMovement = true;
    public float panSpeed = 5000;
    public float panBorderThickness = 10f;

    [Header("Zoom controll")]
    public float scrollSpeed = 5f;
    public float maxY = 80f;
    public float minY = 10f;

    // Update is called once per frame
    void Update() {
        if (GameManager.GameIsOver) {
            Debug.Log("Tu sam");
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            doMovement = !doMovement;
        }

        if (!doMovement) {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
