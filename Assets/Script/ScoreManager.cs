using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    // Use this for initialization
    public int Score;

    public int Offset_Increase_Score;
    public int Offset_Decrease_Score;

    Text _ScoreText;


    void Start()
    {
        _ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    public void IncreaseScore()
    {
        Score += Offset_Increase_Score;
        _ScoreText.text = "Score : " + Score;
    }

    public void DecreaseScore(int num)  //Décremente le score en fonction de l'event.
    {
        if ((Score + Offset_Decrease_Score) < 0)
            Score = 0;
        else
            switch (num)
            {
                case 1:
                    Score += Offset_Decrease_Score;
                    break;

                case 2:
                    Score += Offset_Decrease_Score;
                    break;

                case 3:
                    Score += Offset_Decrease_Score;
                    break;

                case 4:
                    Score += Offset_Decrease_Score;
                    break;

                case 5:
                    Score += Offset_Decrease_Score;
                    break;

                case 6:
                    Score += Offset_Decrease_Score;
                    break;

                case 7:
                    Score += Offset_Decrease_Score;
                    break;

                case 8:
                    Score += Offset_Decrease_Score;
                    break;
            }
        _ScoreText.text = "Score : " + Score;
    }
}
