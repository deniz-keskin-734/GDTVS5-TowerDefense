using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void OnEnable()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.startCoordinates);
        FindPath(true);
    }

    private void FindPath(bool firstPath)//pg32-55
    {
        StopAllCoroutines();
        path.Clear();
        if (firstPath) { path = pathfinder.GetNewPath(); }
        else
        {
            Vector2Int currentPosition = new Vector2Int();
            currentPosition.x = Mathf.RoundToInt(transform.position.x);
            currentPosition.y = Mathf.RoundToInt(transform.position.z);
            path = pathfinder.GetNewPath(currentPosition);
        }
        StartCoroutine(MovementCoroutine());
    }

    IEnumerator MovementCoroutine()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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
