using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Cinemachine;

public class Manager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    int characterIndex;
    private Vector2 lastCheckPosion = new Vector2(-33, 0);

    public static int numberofcoins;
    public TextMeshProUGUI cointsText;
    public CinemachineVirtualCamera VirtualCamera;

    private void Awake()
    {
        numberofcoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter",0);
        Debug.Log(characterIndex);
        GameObject player = Instantiate(playerPrefabs[characterIndex], lastCheckPosion, Quaternion.identity);
        VirtualCamera.m_Follow = player.transform;
    }

    private void Update()
    {
        cointsText.text = ": " + numberofcoins.ToString();
    }
}
