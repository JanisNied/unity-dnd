using System;
using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public TMP_Text timerText;
    public ObjScript objectScript;
    public float totalElapsed;

    void Update()
    {
        if (!objectScript.victoryScreen)
            UpdateTimerUI();
    }

    //call this on update
    public void UpdateTimerUI()
    {
            totalElapsed += Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(totalElapsed);
        string answer = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);
        timerText.text = answer;
        
      
    }
}
