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
       
    }

    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
