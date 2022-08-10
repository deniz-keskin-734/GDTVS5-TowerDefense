using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] public int balance = 225;
    [SerializeField] TMP_Text balanceText;

    private void Start()
    {
        balanceText.text = "Gold: " + balance;
    }

    public void ChangeBalance(int amount)
    {
        balance += amount;
        balanceText.text = "Gold: " + balance;
        if (balance < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
