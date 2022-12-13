
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopWatchScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    private float second;
    public static float second_;
    private int minute;
    private int hour;

    public static string Second;

    // Start is called before the first frame update
    void Start()
    {
        second_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;
        second_ += Time.deltaTime;
        if (minute > 60)
        {
            hour++;
            minute = 0;
        }
        if (second > 60f)
        {
            minute += 1;
            second = 0;
        }

        Second = hour.ToString() + ":" + minute.ToString("00") + ":" + second.ToString("f2");
        timerText.text = Second;
    }
}