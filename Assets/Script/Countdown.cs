using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class Countdown : MonoBehaviour
{
    public int timeLeft = 90; //Seconds Overall
    public Text Timer; //UI Text Object

    bool IsFinish = false;
    bool first = true;

    void Start()
    {
        StartCoroutine("LoseTime");                                         //Lancement compteur.
        Time.timeScale = 1;
    }
    void Update()
    {
        Timer.text = ("" + timeLeft);
        if (timeLeft <= 0 && first != false)
        {
            GameManager.Score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Score;         //Condition de GameOver.
            GameManager.Status = GameState.GameClear;
            first = false;
        }
    }

    IEnumerator LoseTime()
    {
        while (IsFinish == false)
        {
            timeLeft--;
            if (timeLeft <= 0)                                                  //Méthode du Compteur.
                IsFinish = true;
            yield return new WaitForSeconds(1);
        }

    }
}
