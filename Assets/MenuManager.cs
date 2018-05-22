using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public bool MenuIsEnabled = false;
    GameManager _GameManager;
    GameObject _MainMenu;
	// Use this for initialization
	void Start ()
    {
        _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _MainMenu = GameObject.Find("Canvas").transform.Find("MainMenu").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Escape))
            MenuIsEnabled = !MenuIsEnabled;

        if (MenuIsEnabled)
        {
            Time.timeScale = 0f;
            _GameManager.Status = GameState.Pause;
            _MainMenu.SetActive(true);
           
        }
        else
        {
            Time.timeScale = 1f;
            _GameManager.Status = GameState.Playing;
            _MainMenu.SetActive(false);
        }
	}

    public void Resume()
    {
        Time.timeScale = 1f;
        _GameManager.Status = GameState.Playing;
        _MainMenu.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void Exit()
    {
        //SceneManager.LoadScene();
    }
}
