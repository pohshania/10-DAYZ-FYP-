using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    // Day
    [Header("Day Counter")]
    public Text DayCounter;
    [HideInInspector]
    public int Day;

    [Header("Timer")]
    public Text TimerText;
    private float _timer;
    private string _minutes;
    private string _seconds;

    // Debugging purposes
    [Header("Debugging text")]
    private int _increaseRate;
    public Text RateText;

    // This is called before Start()
    private void Awake()
    {
        if (Instance == null) // if instance doesn't contain anything, this script is running for the first time
        {
            Instance = this; // set this curr instance to be contained inside ^ the static instance
            DontDestroyOnLoad(gameObject); // don't detroy the first instance
        }
        else
        {
            Destroy(gameObject); // destory other duplicated instances
        }
    }

    // Use this for initialization
    void Start()
    {
        _timer = Time.time;
        _increaseRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        DayCounts();
    }

    public void Timer()
    {
        float t = (Time.time + _timer) * _increaseRate;

        if (Input.GetKeyDown(KeyCode.T))
        {
            _increaseRate *= 2;
        }
        //Debug.Log(increaseRate);

        _minutes = ((int)t / 60).ToString();
        _seconds = (t % 60).ToString("f0");

        TimerText.text = _minutes + "mins " + _seconds + "sec";

        RateText.text = "Rate: " + _increaseRate;
    }

    public void DayCounts()
    {
        //if (int.Parse(minutes) < 3)
        //{
        //    Debug.Log("DAY 1");
        //}
        //else if (int.Parse(minutes) >= 6)
        //{
        //    Debug.Log("DAY 2");
        //}
        //else if (int.Parse(minutes) >= 9)
        //{
        //    Debug.Log("DAY 3");
        //}
        //else if (int.Parse(minutes) >= 12)
        //{
        //    Debug.Log("DAY 4");
        //}
        //else if (int.Parse(minutes) >= 15)
        //{
        //    Debug.Log("DAY 5");
        //}
        //else if (int.Parse(minutes) >= 18)
        //{
        //    Debug.Log("DAY 6");
        //}
        //else if (int.Parse(minutes) >= 21)
        //{
        //    Debug.Log("DAY 7");
        //}
        //else if (int.Parse(minutes) >= 24)
        //{
        //    Debug.Log("DAY 8");
        //}
        //else if (int.Parse(minutes) >= 27)
        //{
        //    Debug.Log("DAY 9");
        //}
        //else if (int.Parse(minutes) >= 30)
        //{
        //    Debug.Log("DAY 10");
        //}


        //Debug.Log("DAY" + (int.Parse(minutes) / 3));
        Day = ((int.Parse(_minutes) / 3) + 1);
        DayCounter.text = "DAY " + Day;

    }
}
