using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour {

    private GameObject[] characterList;
    public GameObject[] CharacterNames;
    public GameObject[] Characters;

    private int _index;

    // Use this for initialization
    private void Start ()
    {
        _index = PlayerPrefs.GetInt("CharacterSelected");

        characterList = new GameObject[transform.childCount]; // return the number of children to define the size of array
                                                             
        for(int i =0; i<transform.childCount; i++)            // fill the array with the children
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach(GameObject go in characterList)               // turn off all the renderer in the list
        {
            go.SetActive(false);
        }

        if (characterList[0])                                  // if the first child exist in the array 
            characterList[0].SetActive(true);                  // set it to true
        
        if (CharacterNames[0])
            CharacterNames[0].SetActive(true);

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void PressLeft()
    {
        characterList[_index].SetActive(false);               // off the acive of the current character
        CharacterNames[_index].SetActive(false);

        _index -= 1;
        if (_index < 0)
            _index = characterList.Length - 1;

        characterList[_index].SetActive(true);               // off the acive of the next character
        CharacterNames[_index].SetActive(true);
    }

    public void PressRight()
    {
        characterList[_index].SetActive(false);               // off the acive of the current character
        CharacterNames[_index].SetActive(false);

        _index += 1;
        if (_index == characterList.Length)
            _index = 0;

        characterList[_index].SetActive(true);               // off the acive of the next character
        CharacterNames[_index].SetActive(true);
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("CharacterSelected", _index);
        SceneManager.LoadScene("Test");

        Instantiate(Characters[_index]);
    }
}
