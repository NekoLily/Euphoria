using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour {

    GameObject Star1, Star2, Star3, Fermeture, Bienjoue, genial, excellent, score, nbbouteille;

	// Use this for initialization
	void Start () {
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

        score.GetComponent<Text>().text = "Score" + GameManager.current.Score.ToString();

        switch (GameManager.current.Stars)
        {
            case 0:
                Fermeture.SetActive(true);
                break;

            case 1:
                Bienjoue.SetActive(true);
                Star1.SetActive(true);
                break;

            case 2:
                genial.SetActive(true);
                Star1.SetActive(true);
                Star2.SetActive(true);
                break;

            case 3:
                excellent.SetActive(true);
                Star1.SetActive(true);
                Star2.SetActive(true);
                Star3.SetActive(true);
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickEnd(int num)
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
