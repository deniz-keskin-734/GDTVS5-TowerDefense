using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 5;
    const int difficultyRamp = 5;

    int currentHitPoints;
    Enemy enemy;

    private void OnEnable()
    {
        currentHitPoints = hitPoints;
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoints--;
        if(currentHitPoints < 1)
        {
            gameObject.SetActive(false);
            enemy.AddGold();
            hitPoints += difficultyRamp;
        }
    }
}
