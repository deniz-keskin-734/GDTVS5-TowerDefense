using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int activeMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (activeMusicPlayers > 1)
        { 
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
