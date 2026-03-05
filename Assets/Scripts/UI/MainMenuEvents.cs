using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

//reference
//https://www.youtube.com/watch?v=_jtj73lu2Ko
public partial class MainMenuEvents : MonoBehaviour
{
    private UIDocument UIDocMain; // references our main menu doc
    private Button UIButton;
    private List<Button> MainButtons = new List<Button>(); // holds a list of button objects
    private UIDocument UIDocLanding; //takes in landing document

    void Awake()
    {
        UIDocMain = GameObject.Find("MainMenu").GetComponent<UIDocument>(); //grab UIDocument
        UIDocLanding = GameObject.Find("MainMenuLanding").GetComponent<UIDocument>(); //grab landing UIdoc

        UIDocLanding.rootVisualElement.RegisterCallback<ClickEvent>(OnLandingClick); //handles UIlanding click

        //searches for specific button
        UIButton = UIDocMain.rootVisualElement.Q("") as Button;//searches the UI docs main root (highest in the hierarchy) for a visual element in q (query)and cast it as a button
        UIButton.RegisterCallback<ClickEvent>(OnStartClick);

         //grabs every button and turns it to list
        MainButtons = UIDocMain.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < MainButtons.Count; i++)
        {
            MainButtons[i].RegisterCallback<ClickEvent>(OnClickAll);
        }
    }

    private void OnStartClick(ClickEvent evt)
    {

    }
    
    private void OnDisable() //deregisters, good habit
    {

    }

    private void OnClickAll(ClickEvent evt) //can be used for audio and other universal click effects in the main menu
    {
        
    }

    private void OnLandingClick(ClickEvent evt)
    {
        UIDocLanding.enabled = false;
        UIDocMain.enabled = true;
        //play title specific sounds
    }
}
