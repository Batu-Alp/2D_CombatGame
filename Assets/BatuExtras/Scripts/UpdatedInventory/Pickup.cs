using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private Inventory2 inventory2;
    public GameObject itemButton;
    //public bool valid;
    public string itemType;
    //public GameObject effect;

    //[SerializeField] public float value;

    //private PlayerStats healt_value;


    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            healt_value.AddHealth(value);

            
        }
    }*/

    [SerializeField]

    private void Start()
    {
        //valid = false;
        inventory2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // spawn the sun button at the first available inventory slot ! 
            //itemType = other.gameObject.tag;
            //Debug.Log("itemType is : " + itemType);
            //valid = true;
            for (int i = 0; i < inventory2.items.Length; i++)
            {
                if (inventory2.items[i] == 0)
                { // check whether the slot is EMPTY
                    //Instantiate(effect, transform.position, Quaternion.identity);
                    inventory2.items[i] = 1; // makes sure that the slot is now considered FULL
                    Instantiate(itemButton, inventory2.slots[i].transform, false); // spawn the button so that the player can interact with it
                    Destroy(gameObject);
                    break;
                }
            }

            /*if (Input.GetKeyDown(KeyCode.E))
        {

            healt_value.AddHealth(value);

            
        }*/
        }

    }
}
