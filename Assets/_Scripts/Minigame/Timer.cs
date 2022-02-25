using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] float gameTime;
    private float timer;
    private bool stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timer = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60f);
        string textTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timer <= 0)
        {
            stopTimer = true;
            timer = 0;
        }
        if (!stopTimer)
        {
            textDisplay.SetText(textTime);
        }
    }
}
