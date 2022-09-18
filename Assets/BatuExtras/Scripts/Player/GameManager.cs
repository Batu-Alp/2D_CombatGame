using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;
    private bool game_over = false;


    //Fall Detect

    private void Start()
    {
        // CVC = GameObject.Find("2D Camera").GetComponent<CinemachineVirtualCamera>();
        CVC = GameObject.Find("Main Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckRespawn();
    }
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;

    }

    public void EndGame()
    {

        if (game_over == false)
        {

            game_over = true;
            Debug.Log("Game Over");
            Restart();



        }
    }

    void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }



    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }
}
