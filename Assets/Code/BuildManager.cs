using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager Instance;

    void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
    }

    public GameObject basicTurretPrefab;
    public GameObject anotherTurretPrefab;
    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return true; } }
    public bool HasMoney { get { return true; } }

    public void SelectTurretToBuild(TurretBlueprint turret) {
        turretToBuild = turret;
    }

    public void BuildTurretOn(Pedistal pedistal) {
        //if (PlayerStats.Money < 10) {
        //    // TODO not enough money!! show
        //    return;
        //}

        PlayerStats.Money -= 10;

        GameObject turret = (GameObject) Instantiate(basicTurretPrefab, pedistal.GetBuildPosition(), Quaternion.identity);
        pedistal.turret = turret;
    }
}
