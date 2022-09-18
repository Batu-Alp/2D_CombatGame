using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private Transform player;
    //private Transform player;
    public GameObject item;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public void SpawnItem()
    {
        Vector2 playerPos = new Vector2(player.position.x + 3, player.position.y - 0.58f);
        //Vector2 playerPos = new Vector2(player.position.x + 3, player.position.y - 1);

        Instantiate(item, playerPos, Quaternion.identity);

        //Instantiate(item, playerPos.position, Quaternion.identity);
    }
}
