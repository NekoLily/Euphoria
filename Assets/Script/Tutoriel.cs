using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Tutoriel : MonoBehaviour
{

    public GameObject Tuto1, Tuto2, Tuto3, Tuto4, Tuto5, Tuto6;

    // Use this for initialization
    void Start()
    {
        GameManager.Loading.SetActive(false);
        Tuto1 = GameObject.Find("Tuto1");
        Tuto2 = GameObject.Find("Tuto2");
        Tuto3 = GameObject.Find("Tuto3");
        Tuto4 = GameObject.Find("Tuto4");
        Tuto5 = GameObject.Find("Tuto5");
        Tuto6 = GameObject.Find("Tuto6");

        Tuto1.SetActive(true);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
        Tuto4.SetActive(false);
        Tuto5.SetActive(false);
        Tuto6.SetActive(false);

        GameManager.Status = GameState.Tutoriel1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.Status)
        {
            case GameState.Tutoriel1:
                Tuto1.SetActive(true);
                break;

            case GameState.Tutoriel2:
                Tuto1.SetActive(false);
                Tuto2.SetActive(true);
                break;

            case GameState.Tutoriel3:
                Tuto2.SetActive(false);
                Tuto3.SetActive(true);
                break;

            case GameState.Tutoriel4:
                Tuto3.SetActive(false);
                Tuto4.SetActive(true);
                break;

            case GameState.Tutoriel5:
                Tuto4.SetActive(false);
                Tuto5.SetActive(true);
                break;

            case GameState.Tutoriel6:
                Tuto5.SetActive(false);
                Tuto6.SetActive(true);
                break;

            case GameState.FinTuto:
                Tuto6.SetActive(false);
                GameManager.Loading.SetActive(true);
                GameManager.Loading.GetComponent<LoadingScreen>().Loading(0);
                break;
        }
    }

    public void OnClickTuto(int ImageTuto)
    {
        GameObject.Find("TutoManager").GetComponent<AudioSource>().Play();
        switch (ImageTuto)
        {
            case 0:
                GameManager.Status = GameState.Tutoriel1;
                break;

            case 1:
                GameManager.Status = GameState.Tutoriel2;
                break;

            case 2:
                GameManager.Status = GameState.Tutoriel3;
                break;

            case 3:
                GameManager.Status = GameState.Tutoriel4;
                break;

            case 4:
                GameManager.Status = GameState.Tutoriel5;
                break;

            case 5:
                GameManager.Status = GameState.Tutoriel6;
                break;

            case 6:
                GameManager.Status = GameState.FinTuto;
                break;
        }
    }
}
