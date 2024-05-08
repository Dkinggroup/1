using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button[] button;

    private void Awake()
    {
        int unlock = PlayerPrefs.GetInt("Unlock", 1);
        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
        }
        for(int i = 0; i < unlock; i++)
        {
            button[i].interactable = true;
        }
    }

    public void OpenLevel(int LevelID)
    {
        string nameLevel = "Level " + LevelID;
        SceneManager.LoadScene(nameLevel);
    }
}
