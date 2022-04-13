using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    
    void Update()
    {
        float x = player.transform.position.x;
        this.transform.position = new Vector2(x, 3f);
    }
}
