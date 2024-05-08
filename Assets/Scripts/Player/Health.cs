using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int StartHealth;
    [SerializeField] private string startSceneName;

    private int CurrentHealth;

    private void Awake()
    {
        CurrentHealth = StartHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, StartHealth);

        Debug.Log(CurrentHealth);

        if (CurrentHealth < 1)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
