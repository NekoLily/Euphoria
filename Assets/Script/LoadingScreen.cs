using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
    Scene scene;
    AsyncOperation async;
    public Text loadingtext;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Loading(int num)            //en fonction de la situation, lance la méthode pour charger la bonne scene.
    {
        scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "MenuPrincipal":
                StartCoroutine("LoadGame");
                break;
            case "Jeu":
                if (num == 1)
                    StartCoroutine("LoadMainMenu");
                else if (num == 2)
                    StartCoroutine("Replay");
                else
                    StartCoroutine("LoadScore");
                break;
            case "Score":
                if (num == 2)
                    StartCoroutine("Replay");
                else if (num == 1)
                    StartCoroutine("LoadMainMenu");
                else
                    StartCoroutine("LoadNextGame");
                break;
        }
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(1);
        while (!async.isDone)
        {
            loadingtext.color = new Color(loadingtext.color.r, loadingtext.color.g, loadingtext.color.b, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        GameManager.current.StartCoroutine("LoadLevelData");
    }

    IEnumerator ReturnMenu()
    {
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(0);
        while (!async.isDone)
        {
            loadingtext.color = new Color(loadingtext.color.r, loadingtext.color.g, loadingtext.color.b, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        GameManager.Loading.SetActive(false);
        GameManager.Status = GameState.Playing;
    }

    IEnumerator LoadScore()
    {
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(2);
        while (!async.isDone)
        {
            loadingtext.color = new Color(loadingtext.color.r, loadingtext.color.g, loadingtext.color.b, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        GameManager.Loading.SetActive(false);
        GameManager.Status = GameState.Playing;
    }

    IEnumerator Replay()
    {
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(1);
        while (!async.isDone)
        {
            loadingtext.color = new Color(loadingtext.color.r, loadingtext.color.g, loadingtext.color.b, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        GameManager.Loading.SetActive(false);
        GameManager.current.StartCoroutine("LoadLevelData");
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(0);
        while (!async.isDone)
        {
            loadingtext.color = new Color(loadingtext.color.r, loadingtext.color.g, loadingtext.color.b, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        GameManager.Status = GameState.MainMenu;
        GameManager.Loading.SetActive(false);
    }

    IEnumerator LoadNextGame()
    {
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(1);
        while (!async.isDone)
        {
            loadingtext.color = new Color(loadingtext.color.r, loadingtext.color.g, loadingtext.color.b, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
        GameManager.current.LevelChoisi += 1;
        GameManager.current.StartCoroutine("LoadLevelData");
        GameManager.Loading.SetActive(false);
    }
}
