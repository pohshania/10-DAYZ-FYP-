using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentsManager : MonoBehaviour
{
    public static ComponentsManager Instance { get; private set; }

    public Slider HealthBar;
    public Slider HungerBar;
    public Slider ThirstBar;

    private int _decreaseRate = 5;

    private float _rateTimer = 5f;
    private float _rateCD;

    private DayManager theDay;

    // Debugging
    [Header("Decrease rate")]
    public Text DecreaseRateText;

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

    void Start()
    {
        // health component
        HealthBar.maxValue = PlayerManager.Instance.PlayerMaxHealth;
        HealthBar.value = PlayerManager.Instance.PlayerCurrHealth;

        // hunger component
        HungerBar.maxValue = PlayerManager.Instance.PlayerMaxHunger;
        HungerBar.value = PlayerManager.Instance.PlayerCurrHunger;

        // thirst component
        ThirstBar.maxValue = PlayerManager.Instance.PlayerMaxThirst;
        ThirstBar.value = PlayerManager.Instance.PlayerCurrThirst;

        theDay = FindObjectOfType<DayManager>();

    }

    public void Init()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

        //DecreaseRate();

        HealthBar.value = PlayerManager.Instance.PlayerCurrHealth;
        HungerBar.value = PlayerManager.Instance.PlayerCurrHunger;
        ThirstBar.value = PlayerManager.Instance.PlayerCurrThirst;
    }

    void DecreaseRate()
    {
        // day 1 == cd 10f
        // day 2 == cd 
        // day 3 == cd 

        // 0f
        _rateCD -= Time.deltaTime;

        float rate = _rateCD / (float)theDay.Day;

        DecreaseRateText.text = "rate:" + rate;

        if ( rate < 0)
        {
            PlayerManager.Instance.PlayerCurrHealth -= _decreaseRate;
            PlayerManager.Instance.PlayerCurrHunger -= _decreaseRate;
            PlayerManager.Instance.PlayerCurrThirst -= _decreaseRate;

            _rateCD = _rateTimer;
        }

        
    }
}
