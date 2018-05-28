using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public bool MenuIsEnabled = false;
    GameObject _MainMenu;
	// Use this for initialization
	void Start ()
    {
        _MainMenu = GameObject.Find("MainMenu");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Escape))
            MenuIsEnabled = !MenuIsEnabled;

        if (MenuIsEnabled)
        {
            Time.timeScale = 0f;
            GameManager.Status = GameState.Pause;
            _MainMenu.SetActive(true);
           
        }
        else
        {
            Time.timeScale = 1f;
            GameManager.Status = GameState.Playing;
            _MainMenu.SetActive(false);
        }
	}

    public void Resume()
    {
        Time.timeScale = 1f;
        GameManager.Status = GameState.Playing;
        _MainMenu.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void Exit()
    {
        GameManager.Loading.SetActive(true);
        GameManager.Loading.GetComponent<LoadingScreen>().Loading(1);
        GameManager.Status = GameState.Loading;
    }
}
