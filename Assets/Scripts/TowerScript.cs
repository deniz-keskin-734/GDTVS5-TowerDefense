using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour //pg28 35
{
    [SerializeField] GameObject towerWeapon;
    [SerializeField] float weaponRange = 7.5f;
    [SerializeField] ParticleSystem projectiles;
    [SerializeField] int towerCost = 75;
    
    Transform target;

    GameObject[] enemiesInScene;
    float closestDistance;
    float targetsDistance;


    private void Update()
    {
        FindClosestEnemy();
        AimAndFireWeapon();
    }


    internal bool CreateTower(TowerScript towerPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank.balance >= towerCost)
        {
            Instantiate(towerPrefab.gameObject, position, Quaternion.identity);
            bank.ChangeBalance(-towerCost);
            return true;
        }
        else
        {
            return false;
        }
    }


    private void AimAndFireWeapon()
    {
        towerWeapon.transform.LookAt(target);

        if (closestDistance < weaponRange)
        {
            var emissionModule = projectiles.emission;
            emissionModule.enabled = true;
        }
        else
        {
            var emissionModule = projectiles.emission;
            emissionModule.enabled = false;
        }
    }

    private void FindClosestEnemy()
    {
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemiesInScene)
        {
            targetsDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetsDistance < closestDistance)
            {
                target = enemy.transform;
                closestDistance = targetsDistance;
            }
        }
    }
}
