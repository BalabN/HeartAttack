using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    BuildManager buildManager;
    public Turret nanoGunTurret;
    public Turret nanoTeslaTurret;
    public Turret nanoStickTurret;
    public Turret nanoNuclearTurret;

    void Start() {
        buildManager = BuildManager.Instance;
    }

    public void SelectGunTurret() {
        buildManager.SelectTurretToBuild(nanoGunTurret);
    }

    public void SelectTeslaTurret() {
        buildManager.SelectTurretToBuild(nanoTeslaTurret);
    }

    public void SelectStickTurret() {
        buildManager.SelectTurretToBuild(nanoStickTurret);
    }


    public void SelectNuclearTurret() {
        buildManager.SelectTurretToBuild(nanoNuclearTurret);
    }

    public void OnPointerEnter() {
    }
}
