using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Wires the upgrade sydtem to the UI
/// </summary>
public class UpgradeUI : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    public ResourceManager resourceManager;

    private Button upgrade1Button;
    private Label upgrade1Status;
    private Label upgrade1Name;
    private Upgrade upgrade1;

    private void Start()
    {
        UIDocument uiDoc = GameObject.Find("GameUI").GetComponent<UIDocument>();
        if (uiDoc == null)
        {
            Debug.Log("UpgradeUI cant find GameUI");
            return;
        }

        VisualElement root = uiDoc.rootVisualElement;

        upgrade1Button = root.Q<Button>("Upgrade1Button");
        upgrade1Status = root.Q<Label>("Upgrade1Status");
        upgrade1Name = root.Q<Label>("Upgrade1Name");

        if (upgrade1Button == null)
        {
            Debug.Log("cant find Upgrade1Button");
            return;
        }

        upgrade1 = upgradeManager.GetUpgrade("Red Catch Rate I");

        if (upgrade1 != null)
        {
            upgrade1Name.text = upgrade1.Name;
            UpdateStatus();
            Debug.Log("upgrade ui wired to " + upgrade1.Name);
        }

        upgrade1Button.RegisterCallback<ClickEvent>(OnUpgrade1Click);
    }

    private void OnUpgrade1Click(ClickEvent evt)
    {
        if (upgrade1 == null)
            return;

        Debug.Log("upgrade button clicked");
        upgradeManager.PurchaseUpgrade(upgrade1, resourceManager);
        UpdateStatus();

        evt.StopPropagation();
    }

    private void UpdateStatus()
    {
        if (upgrade1.State == UpgradeState.Purchased)
            upgrade1Status.text = "Purchased";
        else
            upgrade1Status.text = "$" + upgrade1.Cost;
    }
}