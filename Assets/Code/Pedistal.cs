using UnityEngine;
using UnityEngine.EventSystems;

public class Pedistal : MonoBehaviour {

    public Color hoverColor;
    public Color noMoneyColor;
    private Vector3 turretOffset;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    void Start() {
        rend = GetComponent<Renderer>();
        turretOffset = new Vector3(0f, rend.bounds.size.y, 0f);
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition() {
        return transform.position + turretOffset;
    }

    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (!BuildManager.Instance.CanBuild) {
            return;
        }

        if (turret != null) {
            // TODO Display on screen
            Debug.Log("Can't build here");
            return;
        }

        BuildManager.Instance.BuildTurretOn(this);
    }

    void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (!BuildManager.Instance.CanBuild) {
            return;
        }

        if (BuildManager.Instance.HasMoney) {
            rend.material.color = hoverColor;
        } else {
            rend.material.color = noMoneyColor;
        }
    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }
}
