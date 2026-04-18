using UnityEngine;
using UnityEngine.UIElements;

//base class for UI systems, currently only going to be upgrades and staff but is scalable

public abstract class UI_Template: MonoBehaviour
{
    public UIDocument UIDoc;

    protected VisualElement root;
    protected VisualElement container;
    protected VisualElement sprite;
    protected Label label;
    protected Button button;

    protected virtual void OnEnable() //virtual enables override
    {
        UIDoc = GetComponent<UIDocument>();
        root = UIDoc.rootVisualElement;

        //add in names post template creation
        container = root.Q <VisualElement>("");
        sprite = root.Q<VisualElement>("");
        label = root.Q<Label>("");
        button = root.Q<Button>("");

        InitializeMenu(); //auto call at the end
    }

    protected abstract void InitializeMenu(); //forces children to create this method


}
