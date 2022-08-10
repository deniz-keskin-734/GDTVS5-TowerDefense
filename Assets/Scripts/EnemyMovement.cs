using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    List<Waypoint> path = new List<Waypoint>();
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        FindPath();
        transform.position = path[0].transform.position;
        StartCoroutine(MovementCoroutine());
    }

    private void FindPath()//pg32
    {
        path.Clear(); 

        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in pathParent.transform)
        {
            path.Add(child.GetComponent<Waypoint>());
        }
    }

    IEnumerator MovementCoroutine()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += 1f * Time.deltaTime;  //pg26
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        gameObject.SetActive(false);
        enemy.StealGold();
    }
}
