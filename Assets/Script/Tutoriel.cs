using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Tutoriel : MonoBehaviour
{
    public static GameManager current;
    public static GameState Status { get; set; }

    public GameObject Tuto1, Tuto2, Tuto3, Tuto4;

	// Use this for initialization
	void Start ()
    {
        Tuto1 = GameObject.Find("Tuto1");
        Tuto2 = GameObject.Find("Tuto2");
        Tuto3 = GameObject.Find("Tuto3");
        Tuto4 = GameObject.Find("Tuto4");

        Tuto1.SetActive(true);
        Tuto2.SetActive(false);
        Tuto3.SetActive(false);
        Tuto4.SetActive(false);

        Status = GameState.Tutoriel1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch (Status)
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

            case GameState.FinTuto:
                Tuto4.SetActive(false);
                //retour menu principal
                break;
        }
	}

    public void OnClickTuto(int ImageTuto)
    {
        switch (ImageTuto)
        {
            case 0:
                Status = GameState.Tutoriel1;
                break;

            case 1:
                Status = GameState.Tutoriel2;
                break;

            case 2:
                Status = GameState.Tutoriel3;
                break;

            case 3:
                Status = GameState.Tutoriel4;
                break;

            case 4:
                Status = GameState.FinTuto;
                break;
        }
    }
}
