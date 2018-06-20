using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour {

    GameObject Star1, Star2, Star3, Fermeture, Bienjoue, genial, excellent, score, nbbouteille;

    public AudioClip good, bad, applause;

	// Use this for initialization
	void Start () {  //Mise en Place de la Scene.
        Star1 = GameObject.Find("1 Star");
        Star2 = GameObject.Find("2 Star");
        Star3 = GameObject.Find("3 Star");
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
        Fermeture = GameObject.Find("Fermeture");
        Bienjoue = GameObject.Find("BienJoué");
        genial = GameObject.Find("Génial");
        excellent = GameObject.Find("Excellent");
        score = GameObject.Find("Score");
        Fermeture.SetActive(false);
        Bienjoue.SetActive(false);
        genial.SetActive(false);
        excellent.SetActive(false);

        score.GetComponent<Text>().text = "Score: " + GameManager.Score.ToString();

        switch (GameManager.Stars)
        {
            case 0:
                GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = bad;
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
                Fermeture.SetActive(true);
                break;

            case 1:
                GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = applause;
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
                Bienjoue.SetActive(true);
                Star1.SetActive(true);
                break;

            case 2:
                GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = applause;
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
                genial.SetActive(true);
                Star1.SetActive(true);
                Star2.SetActive(true);
                break;

            case 3:
                GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = applause;
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
                excellent.SetActive(true);
                Star1.SetActive(true);
                GameObject.Find("Canvas").GetComponent<AudioSource>().PlayOneShot(good);
                Star2.SetActive(true);
                Star3.SetActive(true);
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickEnd(int num) //méthode des boutons de la Scene.
    {
        switch(num)
        {
            case 1:
                GameManager.Loading.SetActive(true);
                GameManager.Loading.GetComponent<LoadingScreen>().Loading(2);  //Replay
                GameManager.Status = GameState.Loading;
                break;

            case 2:
                GameManager.Loading.SetActive(true);
                GameManager.Loading.GetComponent<LoadingScreen>().Loading(1);   //Menu
                GameManager.Status = GameState.Loading;
                break;

            case 3:                                         //NExtLEvel
                GameManager.Loading.SetActive(true);
                GameManager.Loading.GetComponent<LoadingScreen>().Loading(0);
                GameManager.Status = GameState.Loading;
                break;
        }
    }
}
