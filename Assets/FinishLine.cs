using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class FinishLine : MonoBehaviour
{





    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("FinishScene");

        }

    }

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
