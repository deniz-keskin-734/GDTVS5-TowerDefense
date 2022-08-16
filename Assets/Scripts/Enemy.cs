using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyScore = 10;
    [SerializeField] ParticleSystem explosionPrefab;

    GameManager gameManager;
    List<ParticleSystem> explosions;
    List<MeshRenderer> childWingMeshes;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        explosions = new List<ParticleSystem>();
        childWingMeshes = new List<MeshRenderer>();
        GetWingMeshes();
    }

    private void OnParticleCollision(GameObject other)
    {//pg11
        Disappear();
        gameManager.IncreaseScore(enemyScore);
        explosions.Add(Instantiate(explosionPrefab, transform.position, Quaternion.identity));
        explosions[0].Play();
        StartCoroutine(EnemyExplosionCoroutine());
    }

    private void GetWingMeshes()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "EnemyWing")
            {
                childWingMeshes.Add(child.gameObject.GetComponent<MeshRenderer>());
            }
        }
    }

    private void Disappear()
    {
        GetComponentInChildren<BoxCollider>().enabled = false; //change
        GetComponent<MeshRenderer>().enabled = false;
        foreach (var wingMesh in childWingMeshes) 
        { 
            wingMesh.enabled = false; 
        }
    }

    IEnumerator EnemyExplosionCoroutine()
    {
        yield return new WaitForSeconds(1);
        foreach (ParticleSystem explosion in explosions) { Destroy(explosion.gameObject); }
        Destroy(this.gameObject);
    }
}
