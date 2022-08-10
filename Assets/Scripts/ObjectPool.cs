using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    GameObject[] objPool;


    private void Awake()
    {
        objPool = new GameObject[poolSize];
        for(int i = 0; i < poolSize; i++)
        {
            objPool[i] = Instantiate(enemyPrefab, transform);
            objPool[i].SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(EnemySpawnCoroutine());
    }

    IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < poolSize; i++)//pg34
            {
                if (!objPool[i].activeInHierarchy)
                {
                    objPool[i].SetActive(true);
                    break;
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
