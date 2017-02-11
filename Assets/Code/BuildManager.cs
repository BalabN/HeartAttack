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
    private Pedestal selectedPedestal;

    public PedestalUI pedestalUI;

    public bool CanBuild { get { return turretToBuild == null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectPedestal(Pedestal pedestal) {
        if (selectedPedestal == pedestal) {
            DeselectPedestal();
            return;
        }
        selectedPedestal = pedestal;
        turretToBuild = null;

        pedestalUI.OpenUI(pedestal);
    }

    public void DeselectPedestal() {
        selectedPedestal = null;
        pedestalUI.Hide();
    }

    public void SelectTurretToBuild(Turret turret) {
        turretToBuild = turret;
    }

    public Turret GetTurretToBuild() {
        return turretToBuild;
    }

    public Pedestal GetSelectedPedestal() {
        return selectedPedestal;
    }
}
