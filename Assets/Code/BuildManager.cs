using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager Instance;

    void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;
    }

    public GameObject turretNanoGun;
    public GameObject turretNanoTesla;
    public GameObject turretNanoStick;
    public GameObject turretNanoNuclear;

    private Turret turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectTurretToBuild(Turret turret) {
        turretToBuild = turret;
    }

    public void BuildTurretOn(Pedistal pedistal) {
        if (PlayerStats.Money < turretToBuild.cost) {
            // TODO not enough money!! show
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, pedistal.GetBuildPosition(), Quaternion.identity);
        pedistal.turret = turret;
    }
}
