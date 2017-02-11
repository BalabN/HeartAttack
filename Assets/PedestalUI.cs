using UnityEngine;
using UnityEngine.UI;

public class PedestalUI : MonoBehaviour {

    public GameObject ui;
    public GameObject shop;
    public GameObject upgrade;

    [Header("Upgrade")]
    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    Pedestal Target;

    public void OpenUI(Pedestal target) {
        Target = target;

        transform.position = target.GetBuildPosition();

        Debug.Log(target.isUpgraded);

        GameObject goToShow = upgrade;
        if (target.turret == null) {
            goToShow = shop;
        } else if (!target.isUpgraded) {
            upgradeCost.text = target.turretBlueprint.upgradeCost + "";
            if (PlayerStats.Money < target.turretBlueprint.upgradeCost) {
                upgradeButton.interactable = false;
            } else {
                upgradeButton.interactable = true;
            }
            sellAmount.text = target.turretBlueprint.sellAmount + "";
        } else {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
            sellAmount.text = target.turretBlueprint.sellAmount + "";
        }

        Show(goToShow);
        ui.SetActive(true);
    }

    public void Hide() {
        shop.SetActive(false);
        upgrade.SetActive(false);
        ui.SetActive(false);
    }

    public void Upgrade() {
        Target.UpgradeTurret();
        BuildManager.Instance.DeselectPedestal();
    }

    public void Sell() {
        Target.SellTurret();
        BuildManager.Instance.DeselectPedestal();
    }

    public void Show(GameObject uiGO) {
        uiGO.SetActive(true);
    }

}
