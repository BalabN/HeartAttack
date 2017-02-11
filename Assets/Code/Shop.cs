using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    BuildManager buildManager;
    public Turret nanoGunTurret;
    public Turret nanoTeslaTurret;
    public Turret nanoStickTurret;
    public Turret nanoNuclearTurret;

    [Header("Shop")]
    public Text gunCost;
    public Text teslaCost;
    public Text stickCost;
    public Text nuclearCost;

    void Start() {
        buildManager = BuildManager.Instance;
        gunCost.text = nanoGunTurret.cost + "";
        teslaCost.text = nanoTeslaTurret.cost + "";
        stickCost.text = nanoStickTurret.cost + "";
        nuclearCost.text = nanoNuclearTurret.cost + "";
    }

    public void BuildGunTurret() {
        BuildTurret(nanoGunTurret);
    }

    public void BuildTeslaTurret() {
        BuildTurret(nanoTeslaTurret);
    }

    public void BuildStickTurret() {
        BuildTurret(nanoStickTurret);
    }


    public void BuildNuclearTurret() {
        BuildTurret(nanoNuclearTurret);
    }

    void BuildTurret(Turret turret) {
        buildManager.SelectTurretToBuild(turret);
        buildManager.GetSelectedPedestal().BuildTurret(turret);
        buildManager.DeselectPedestal();
    }

    public void OnPointerEnter() {
    }
}
