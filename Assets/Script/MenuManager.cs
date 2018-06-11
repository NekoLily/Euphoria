using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {      //Script Menu Pause: Activation et Boutons.

    public bool MenuIsEnabled = false;
    public bool MenuRIsEnabled = false;
    GameObject _MainMenu, _RecettesMenu;
    List<GameObject> Menu;
	// Use this for initialization
	void Start ()
    {
        _MainMenu = GameObject.Find("MainMenu");
        _RecettesMenu = GameObject.Find("Recettes");
        Menu = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bouton"));
        int x = Menu.Count;
        for (int y = 0; y < x; y++)
        {
            Debug.Log(Menu[y]);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Escape))
            MenuIsEnabled = !MenuIsEnabled;

        if (_RecettesMenu.activeInHierarchy)
            MenuRIsEnabled = true;
        else
            MenuRIsEnabled = false;

        if (MenuIsEnabled)
        {
            foreach (var item in Menu)
            {
                item.GetComponent<Button>().interactable = false;
            }
            Time.timeScale = 0f;
            GameManager.Status = GameState.Pause;
            _MainMenu.SetActive(true);
           
        }
        else
        {
            Time.timeScale = 1f;
            foreach (var item in Menu)
            {
                item.GetComponent<Button>().interactable = true;
            }
            GameManager.Status = GameState.Playing;
            _MainMenu.SetActive(false);
        }

        if (MenuRIsEnabled)
        {
            foreach (var item in Menu)
            {
                item.GetComponent<Button>().interactable = false;
            }

        }
        else
        {
            foreach (var item in Menu)
            {
                item.GetComponent<Button>().interactable = true;
            }
        }
	}

    public void ReturnOnClick()
    {
        _RecettesMenu.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameManager.Status = GameState.Playing;
        _MainMenu.SetActive(false);
    }

    public void Retry()
    {
        GameManager.Loading.SetActive(true);
        GameManager.Loading.GetComponent<LoadingScreen>().Loading(2);  //Replay
        GameManager.Status = GameState.Loading;
        MenuIsEnabled = false;

    }

    public void Exit()
    {
        GameManager.Loading.SetActive(true);
        GameManager.Loading.GetComponent<LoadingScreen>().Loading(1);
        GameManager.Status = GameState.Loading;
    }
}
