using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamController : MonoBehaviour
{
    private Transform player;

    private float yOffset = 3f;
    private float zOffset = -7f;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z + zOffset);
    }

}
