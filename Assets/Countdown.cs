using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class Countdown : MonoBehaviour
{
    public int timeLeft = 90; //Seconds Overall
    public Text Timer; //UI Text Object
    void Start()
    {
        StartCoroutine("LoadScene2");
        Time.timeScale = 1; 
    }
    void Update()
    {
        Timer.text = ("" + timeLeft);
        Timer = GameObject.Find("Temps").GetComponent<Text>(); // va chercher le game object temps et le text 
    }
    
    IEnumerator LoseTime()
    {
        while (true)
        {
            timeLeft--;
            yield return new WaitForSeconds(1);
        }
    }
}
