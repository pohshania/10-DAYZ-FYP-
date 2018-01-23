using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [Header("Resource Info")]
    public string ResourceName;
    public int ResourceCost;
    public enum RESOURCE_TYPE
    {
        HEALTH = 0,
        HUNGER,
        THIRST,
        NONE
    }


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
