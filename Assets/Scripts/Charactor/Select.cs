using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public GameObject[] character;
    public int selectCharacter;
/*    public Character[] characters;*/

    private void Start()
    {
        selectCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        if (selectCharacter >= character.Length)
        {
            selectCharacter = 0;
        }

        if (selectCharacter < 0)
        {
            selectCharacter = character.Length - 1;
        }

        foreach (GameObject player in character)
        {
            player.SetActive(false);
        }

        character[selectCharacter].SetActive(true);

/*        foreach(Character c in characters)
        {
            if (c.price == 0)
                c.isUnlocked = true;
            else 
            {
                c.isUnlocked = PlayerPrefs.GetInt(c.name, 0) == 0 ? false : true;
            }
        }*/
    }

    public void ChangeNext()
    {
        character[selectCharacter].SetActive(false);
        selectCharacter++;
        if(selectCharacter >= character.Length)
        {
            selectCharacter = 0;
        }

        character[selectCharacter].SetActive(true);

        PlayerPrefs.SetInt("SelectedCharacter", selectCharacter);
    }

    public void ChangePrevious()
    {
        character[selectCharacter].SetActive(false);
        selectCharacter--;
        if (selectCharacter < 0)
        {
            selectCharacter = character.Length - 1;
        }

        character[selectCharacter].SetActive(true);

        PlayerPrefs.SetInt("SelectedCharacter", selectCharacter);
    }
}
