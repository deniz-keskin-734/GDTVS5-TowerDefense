using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField][Range(0, 500)] int goldRewardOnDestroy = 25;
    [SerializeField][Range(-500,0)] int goldPenaltyOnFail = -25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void AddGold()
    {
        bank.ChangeBalance(goldRewardOnDestroy);
    }

    public void StealGold()
    {
        bank.ChangeBalance(goldPenaltyOnFail);
    }
}
