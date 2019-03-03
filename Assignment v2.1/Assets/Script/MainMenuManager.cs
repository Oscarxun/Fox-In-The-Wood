using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public Animator SettingPanel;
    public Animator MainMenuPanel;
    public Animator ControlsMenuPanel;
    public GameObject QuitDialog;
    public Animator LoadPanel;

    EventSystem m_EventSystem;

    public GameObject defaultSelectButton;
    // Start is called before the first frame update
    
    void Start()
    {
        Time.timeScale = 1;
        SettingPanel = SettingPanel.GetComponent<Animator>();
        MainMenuPanel = MainMenuPanel.GetComponent<Animator>();
        ControlsMenuPanel = ControlsMenuPanel.GetComponent<Animator>();
        defaultSelectButton = defaultSelectButton.GetComponent<GameObject>();

        m_EventSystem = EventSystem.current;
        if (GameData.LevelCompleted == 0 || GameData.LevelCompleted == 1)
        {
            GameData.LevelCompleted = 1;
        }
            //GameData.LevelCompleted = 2;
            if (GameData.LevelCompleted != 0 || GameData.LevelCompleted != 1)
        {
            for (int i = 1; i < GameData.LevelCompleted; i++)
            {
                GameObject y = GameObject.Find("Level " + (i + 1) + " Locked");
                //GameObject x = GameObject.Find("Level " + (i + 1) + " Btn");
                y.SetActive(false);
                //x.SetActive(true);
            }

            
        }

        for (int i = GameData.LevelCompleted; i < 2; i++)
        {
            //GameObject y = GameObject.Find("Level " + (i + 1) + " Locked");
            GameObject x = GameObject.Find("Level " + (i + 1) + " Btn");
            //y.SetActive(false);
            x.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        SceneManager.LoadScene(1);
        GameData.Level = 1;
    }

    public void OpenLoad()
    {
        MainMenuPanel.enabled = true;
        LoadPanel.enabled = true;
        MainMenuPanel.SetBool("isHidden", true);
        LoadPanel.SetBool("isHidden", false);
    }

    public void CloseLoad()
    {
        MainMenuPanel.SetBool("isHidden", false);
        LoadPanel.SetBool("isHidden", true);
    }

    public void OpenSetting()
    {
        MainMenuPanel.enabled = true;
        SettingPanel.enabled = true;
        MainMenuPanel.SetBool("isHidden", true);
        SettingPanel.SetBool("isHidden", false);
        
    }

    public void CloseSetting()
    {
        
        MainMenuPanel.SetBool("isHidden", false);
        SettingPanel.SetBool("isHidden", true);
        m_EventSystem.SetSelectedGameObject(defaultSelectButton);
    }

    public void OpenControls()
    {
        MainMenuPanel.enabled = true;
        ControlsMenuPanel.enabled = true;
        MainMenuPanel.SetBool("isHidden", true);
        ControlsMenuPanel.SetBool("isHidden", false);
    }

    public void CloseControls()
    {
        MainMenuPanel.SetBool("isHidden", false);
        ControlsMenuPanel.SetBool("isHidden", true);
    }

    public void OpenQuitDialog()
    {
        QuitDialog.SetActive(true);
    }

    public void CloseQuitDialog()
    {
        QuitDialog.SetActive(false);
    }

    public void LevelSelect()
    {
        
        int level = int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring(6,1));
        Debug.Log(level);

        

        GameData.Level = level;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
