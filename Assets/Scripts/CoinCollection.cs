using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Manager.numberofcoins++;
            PlayerPrefs.SetInt("NumberOfCoins", Manager.numberofcoins);
            Destroy(gameObject);
        }
    }
}
