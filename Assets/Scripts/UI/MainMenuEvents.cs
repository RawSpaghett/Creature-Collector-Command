using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

//reference
//https://www.youtube.com/watch?v=_jtj73lu2Ko
public partial class MainMenuEvents : MonoBehaviour
{
    private UIDocument UIDocMain; // references our main menu doc

    private Button UIButton;

    private List<Button> MainButtons = new List<Button>(); // holds a list of button objects

    void Awake()
    {
        UIDocMain = GetComponent<UIDocument>(); //grab UIDocument

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


    void OnStartClick(ClickEvent evt)
    {}
    
    //deregisters, good habit
    private void OnDisable()
    {

    }

    private void OnClickAll(ClickEvent evt) //can be used for audio and other universal click effects
    {
    }
}
