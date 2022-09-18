using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    //[SerializeField]
    //private GameObject deathChunkParticle, deathBloodParticle;

    [SerializeField]
    public float currentHealth;

    //private float currentHealth;

    private GameManager GM;

    public HealthBar health_bar;

    // GodMod
    private bool god_mod_on;
    //[SerializeField] private int time;
    private Slot slotObj;








    private void Start()
    {
        currentHealth = maxHealth;
        health_bar.SetMaxHealth(maxHealth);
        //StartCoroutine(Counter());
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        slotObj = GameObject.FindGameObjectWithTag("Slot").GetComponent<Slot>();
    }


    /*public void AddHealth(float value)
    {
        //currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        currentHealth += value;
        health_bar.SetHealth(currentHealth);

    }*/

    public void AddHealth(float value)
    {
        float temp = currentHealth;
        if (currentHealth != maxHealth)
        {

            currentHealth += value;
            health_bar.SetHealth(currentHealth);
        }

        else
        {
            currentHealth = temp;
        }


    }

    /*public IEnumerator Counter()
    {

        god_mod_on = true;
        God_Mod();
        yield return new WaitForSeconds(time);
        god_mod_on = false;
        God_Mod();
    }*/


    /*private void God_Mod()
    {

        float lastest_health = currentHealth;

        if (god_mod_on == true)
        {

            currentHealth = 10000;
        }

        else
        {

            currentHealth = lastest_health;

        }

    }*/

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        health_bar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            FindObjectOfType<GameManager>().EndGame();



        }

        /*else
        {
            health_bar.SetHealth(currentHealth);

        }*/

        /*currentHealth -= amount;
        health_bar.SetHealth(currentHealth);

        if (currentHealth <= 0.0f)
        {
            Die();
        }*/

        //---------------
        /*if (currentHealth <= 0.0f)

        {
            Die();
            //Destroy(gameObject);
        }

        else
        {
            currentHealth -= amount;
            health_bar.SetHealth(currentHealth);

        }*/

        //-------------

        /*if (currentHealth > 0.0f)
        {
            //currentHealth -= amount;
            health_bar.SetHealth(currentHealth);
        }

        else
        {
            Die();
            //GameOverScreen();

        }*/
    }

    private void Die()
    {
        //Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        //Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        //slotObj.RemoveItem();
        Debug.Log("Dead");


        //GM.Respawn();

        /*if (gameObject != null)
        {
            Destroy(gameObject);
        }*/
        //slotObj.RemoveItem();
        Destroy(gameObject);
        //slotObj.RemoveItem();

    }


}
