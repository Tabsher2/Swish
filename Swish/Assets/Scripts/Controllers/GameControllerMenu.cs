using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerMenu : MonoBehaviour {

    public GameObject homeMenu;
    public GameObject menu1Menu;
    public GameObject menu2Menu;
    public GameObject menu4Menu;
    public GameObject menu5Menu;
    public GameObject activeMenu;
    public GameObject gameLoadingPanel;

    public Button practice;
    public Button play;

    public Toggle button1;
    public Toggle button2;
    public Toggle buttonHome;
    public Toggle button4;
    public Toggle button5;

    bool flagSceneLoading = true; //flag to only allow one scene to load


    // Use this for initialization
    void Awake ()
    {
        homeMenu.SetActive(true);
        activeMenu = homeMenu;
        menu1Menu.SetActive(false);
        menu2Menu.SetActive(false);
        menu4Menu.SetActive(false);
        menu5Menu.SetActive(false);

        practice.onClick.AddListener(() => PracticeClicked());
        play.onClick.AddListener(() => PlayClicked());
    }

    void Start()
    {
        buttonHome.Select();
    }

    // Update is called once per frame
    void Update ()
    {
        if(button1.isOn)
        {
            activeMenu.SetActive(false);
            menu1Menu.SetActive(true);
            activeMenu = menu1Menu;
        }
        if (button2.isOn)
        {
            activeMenu.SetActive(false);
            menu2Menu.SetActive(true);
            activeMenu = menu2Menu;
        }
        if (buttonHome.isOn)
        {
            activeMenu.SetActive(false);
            homeMenu.SetActive(true);
            activeMenu = homeMenu;
        }
        if (button4.isOn)
        {
            activeMenu.SetActive(false);
            menu4Menu.SetActive(true);
            activeMenu = menu4Menu;
        }
        if (button5.isOn)
        {
            activeMenu.SetActive(false);
            menu5Menu.SetActive(true);
            activeMenu = menu5Menu;
        }

        

    }



    void PracticeClicked()
    {
        gameLoadingPanel.SetActive(true);
        practice.onClick.RemoveListener(() => PracticeClicked());
     
        SceneManager.LoadScene("Practice", LoadSceneMode.Single);
     

    }
    void PlayClicked()
    {
        gameLoadingPanel.SetActive(true);
        play.onClick.RemoveListener(() => PlayClicked());

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);


    }
}
