using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class GameEvents : MonoBehaviour //handles UI input
{
    private UIDocument UIDocGame; // references our main menu doc
    private VisualElement UIButton;
    private Label resourceLabel;
    public int ResourceNum = 0;

   
   void OnEnable()
   {
        UIDocGame = GameObject.Find("GameUI").GetComponent<UIDocument>(); //grab UIDocument
        resourceLabel = UIDocGame.rootVisualElement.Q<Label>("Resource"); //grab resource Label
        
        UIButton = UIDocGame.rootVisualElement.Q("CreatureClick");
        UIButton.RegisterCallback<PointerDownEvent>(OnCreatureClick);

        Debug.Log("OnEnable Go");
   }

   private void OnDisable() //deregisters, good habit
    {

    }

   void OnCreatureClick(PointerDownEvent evt) //speak to CreatureEventManager, but for now will just add numbers on click
   {
    ResourceNum++;
    resourceLabel.text = ("Score: " + ResourceNum.ToString());
    Debug.Log("OnClick Go");
   }

}
