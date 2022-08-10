using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class SetTileText : MonoBehaviour
{

    TextMeshPro label;
    Vector2Int position = new Vector2Int();

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
        label.enabled = false;
        transform.parent.name = position.ToString(); 
    }

    void Update()
    {
        if(Application.isEditor && !Application.isPlaying)
        {
            DisplayCoordinates();
        }
        ToggleCoordinates();
    }

    private void ToggleCoordinates()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void DisplayCoordinates()
    {
        position.x = Mathf.RoundToInt(transform.parent.position.x);
        position.y = Mathf.RoundToInt(transform.parent.position.z);
        label.text = position.ToString();

    }
}
