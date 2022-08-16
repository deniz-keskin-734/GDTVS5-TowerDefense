using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionFX;
    private void OnTriggerEnter(Collider other)
    {//pg8
        GetComponent<PlayerControl>().enabled = false;
        StartCoroutine(Wait1SecCoroutine());
        explosionFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
    }

    IEnumerator Wait1SecCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
