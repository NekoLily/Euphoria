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

    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
    }
    void Update()
    {
        Timer.text = ("" + timeLeft);
        if (timeLeft <= 0)
        {
            GameManager.current.Score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().Score;
            GameManager.Status = GameState.GameClear;
        }
    }

    IEnumerator LoseTime()
    {
        while (IsFinish == false)
        {
            timeLeft--;
            if (timeLeft <= 0)
                IsFinish = true;
            yield return new WaitForSeconds(1);
        }

    }
}
