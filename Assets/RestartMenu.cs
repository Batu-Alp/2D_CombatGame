using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RestartMenu : MonoBehaviour
{

    //[SerializeField] public Text text1;
    //[SerializeField] public Text text2;

    /*[SerializeField] public TMP_Text text1;
    [SerializeField] public TMP_Text text2;

    private PlayerController player;*/

    /*void Start()
    {

        //player = FindObjectOfType<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


    

    }*/

    /*void Update()
    {

        //Debug.Log("Money num is :" + player.Money);
        if (player.Money == 5)
        {

            text1.gameObject.SetActive(true);
        }

        else
        {
            text2.gameObject.SetActive(true);

        }

    }*/



    public void PlayGame()
    {

        SceneManager.LoadScene("Startmenu");
    }

    public void QuitGame()
    {

        Application.Quit();
        //Debug.Log("Quit");
    }



}
