using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float timeInSeconds;
    private string format = "{0:00}";
    private float secondsToText;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeInSeconds += Time.deltaTime;
        secondsToText += Time.deltaTime;
        if (secondsToText >= 60f)
        {
            secondsToText = 0f;
        }


        int totalSeconds = Mathf.FloorToInt(timeInSeconds);
        int seconds = Mathf.FloorToInt(secondsToText);
        int minutes = totalSeconds / 60;

        text.text = string.Format(format, minutes) + ":" + string.Format(format, seconds);


    }
}
