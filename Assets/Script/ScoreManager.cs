using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    // Use this for initialization
    public int Score;

    public int Offset_Increase_Score;
    public int Offset_Decrease_Score;
    public int Offset_Decrease_Score2;

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

    public void DecreaseScore()
    {
        Score += Offset_Decrease_Score;
        _ScoreText.text = "Score : " + Score;
    }

    public void DecreaseScoreBagarre()
    {
        Score += Offset_Decrease_Score2;
        _ScoreText.text = "Score : " + Score;
    }
}
