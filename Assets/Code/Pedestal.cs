using UnityEngine;
using UnityEngine.EventSystems;

public class Pedestal : MonoBehaviour {

    public Color hoverColor;
    public Color noMoneyColor;
    private Vector3 turretOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public Turret turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

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

        Debug.Log("Can't build here3");
        BuildManager.Instance.SelectPedestal(this);


    }

    public void BuildTurret(Turret blueprint) {
        if (PlayerStats.Money < blueprint.cost) {
            // TODO not enough money!! show
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject) Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
    }

    public void UpgradeTurret() {
        if (PlayerStats.Money < turretBlueprint.upgradeCost) {
            // TODO not enough money!! show
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Old turret
        Destroy(turret);

        // Upgraded build
        GameObject _turret = (GameObject) Instantiate(turretBlueprint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        isUpgraded = true;
    }

    public void SellTurret() {
        PlayerStats.Money += turretBlueprint.sellAmount;

        Destroy(turret);
        turret = null;
        isUpgraded = false;
        turretBlueprint = null;
    }

    void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (!BuildManager.Instance.CanBuild) {
            return;
        }

        //if (BuildManager.Instance.HasMoney) {
            rend.material.color = hoverColor;
        //} else {
            //rend.material.color = noMoneyColor;
        //}
    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }
}
