using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{


    private Inventory2 inventory;
    public int index;

    private PlayerStats stat;
    private PlayerController control;
    private PlayerCombatController combat_control;

    //private HealthItem obj;
    private Pickup obj;
    private Pickup obj2;
    private Pickup obj3;

    private bool itemIsUsed;
    //private bool valid;


    //public GameObject h;
    //public GameObject j;


    private void Start()
    {

        List<Transform> children = new();

        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            children.Add(child);
        }

        Debug.Log("Count: " + children.Count);


        //h = GameObject.FindWithTag("Health");

        //j = GameObject.FindWithTag("Jump");
        itemIsUsed = false;

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory2>();

        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        //obj = GameObject.FindGameObjectWithTag("PotionItem").GetComponent<Pickup>();
        // obj = GameObject.FindGameObjectWithTag("Health").GetComponent<Pickup>();
        //obj2 = GameObject.FindGameObjectWithTag("JumpPotion").GetComponent<Pickup>();
        //obj3 = GameObject.FindGameObjectWithTag("SpeedPotion").GetComponent<Pickup>();

        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        combat_control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>();

    }

    private void Update()
    {

        /*if(Input.GetMouseButtonDown(0)){
            UseItem();
        }*/
        //Debug.Log("sayı : " + gameObject.child.name);

        /*foreach (Transform t in transform)
        {
            Debug.Log("Found " + t);

            if (t.gameObject.name == "Child")
            {
                Debug.Log("Found " + t);
            }
        }*/


        if (transform.childCount <= 0)
        {
            inventory.items[index] = 0;
        }

        /*if (itemIsUsed == true)
        {
            Destroy(gameObject);
        }*/

        /*if (Input.GetButtonDown("PotionButton")){

            GameObject potion = FindItemByType("Health Potion");
            if (potion != null) {
                stat.AddHealth(10);

            }


        }*/
    }

    /*private void ClickToUse()
    {

     
        if (itemIsUsed != true)

        {
            stat.AddHealth(10);
            itemIsUsed = true;
            RemoveItem();
        }

        itemIsUsed = false;
    }*/

    public void ClickToUse()
    {

        foreach (Transform child in transform)
        {
            //Debug.Log("name : " + child);
            Debug.Log("name : " + child.name);
            Debug.Log("Type is : " + child.name);
            Debug.Log("Tag is : " + child.tag);



            if (itemIsUsed != true && child.tag == "Health")
            {
                stat.AddHealth(10);
                itemIsUsed = true;
                RemoveItem();
            }

            else if (itemIsUsed != true && child.tag == "JumpPotion")

            {
                control.AddJumpBoost(3);
                itemIsUsed = true;
                RemoveItem();
            }

            else if (itemIsUsed != true && child.tag == "SpeedPotion")

            {
                control.SpeedingBoost(2);
                itemIsUsed = true;
                RemoveItem();
            }

            else if (itemIsUsed != true && child.tag == "DamagePotion")

            {
                combat_control.DamageBoost(3);
                itemIsUsed = true;
                RemoveItem();
            }

            else
            {
                itemIsUsed = false;
            }


        }

        /*foreach (Transform child in transform)
        {
            if (child.GetComponent<Slot>().itemIsUsed != true)
            {
                child.GetComponent<PlayerStats>().AddHealth(10);
                child.GetComponent<Slot>().itemIsUsed = true;
                child.GetComponent<Slot>().RemoveItem();
            }
        }*/

        // ----------------------------------

        /*Debug.Log("The obj.itemType is " + obj.itemType);
        Debug.Log("The obj2.itemType is " + obj.itemType);
        Debug.Log("The obj3.itemType is " + obj.itemType);

        if (itemIsUsed != true && obj.itemType == "HealthPotion")
        {
            stat.AddHealth(10);
            itemIsUsed = true;
            RemoveItem();
        }

        else if (itemIsUsed != true && obj.itemType == "JumpBoost")

        {
            control.AddJumpBoost(3);
            itemIsUsed = true;
            RemoveItem();
        }

        else if (itemIsUsed != true && obj.itemType == "SpeedBoost")

        {
            control.SpeedingBoost(2);
            itemIsUsed = true;
            RemoveItem();
        }

        else if (itemIsUsed != true && obj.itemType == "DamagePotion")

        {
            combat_control.DamageBoost(3);
            itemIsUsed = true;
            RemoveItem();
        }*/

        // ----------------------------------


        /*if (itemIsUsed != true)
        {

            if (obj.itemType == "HealthPotion")
            {
                stat.AddHealth(10);
                itemIsUsed = true;
                RemoveItem();
            }

            else if (obj2.itemType == "JumpBoost")

            {
                control.AddJumpBoost(3);
                itemIsUsed = true;
                RemoveItem();
            }

            else if (obj3.itemType == "SpeedBoost")

            {
                control.SpeedingBoost(2);
                itemIsUsed = true;
                RemoveItem();
            }



        }*/

        //obj.Use();
        //if (obj.valid == true && itemIsUsed != true)
        /*if (obj.itemType == "HealthPotion" && itemIsUsed != true)

        {
            stat.AddHealth(10);
            //control.AddJumpBoost(3);
            itemIsUsed = true;
            RemoveItem();
        }

        if (obj2.itemType == "JumpBoost" && itemIsUsed != true)

        {
            control.AddJumpBoost(3);
            itemIsUsed = true;
            RemoveItem();
        }

        if (obj3.itemType == "SpeedBoost" && itemIsUsed != true)

        {
            control.SpeedingBoost(2);
            //stat.AddHealth(10);
            //control.AddJumpBoost(3);
            itemIsUsed = true;
            RemoveItem();
        }*/

        /*else
        {
            itemIsUsed = false;
        }*/

        //itemIsUsed = false;




        //GameObject.Destroy(gameObject);
        /*foreach (Transform child in transform)
        {
            //child.GetComponent<Spawn>().SpawnItem();
            child.GetComponent<PlayerStats>().AddHealth(10);
            GameObject.Destroy(child.gameObject);
        }*/


        /*if (Input.GetMouseButton(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            Debug.Log("Touched");
            obj.Use();

        }*/

    }


    /* public int FindItemByType(string itemType) {

         for (int i = 0; i < inventory.items.Length; i++ ) {

             if (inventory.items[i] != null) {
                 if (inventory.GetComponent<Pickup>().itemType == itemType) {
                     return inventory.items[i];
                 }
             }
         }
         return null;
     }*/

    /*public void UseItem() {


         if(Input.GetMouseButtonDown(0)){
             //UseItem();
         
 

             //if ( child.GameObject.FindGameObjectWithTag("Health").GetComponent<Inventory2>()){
            if (h) {
                stat.AddHealth(10);
                GameObject.Destroy(gameObject);

            }
        
            //if (child.GameObject.FindGameObjectWithTag("Jump").GetComponent<Inventory2>()){
            if (j){

                    cont.AddJumpBoost(10);
                    GameObject.Destroy(gameObject);

             }

         }

       
    }*/

    public void RemoveItem()
    {

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);

        }
        //itemIsUsed = false;

    }

    public void Cross()
    {

        foreach (Transform child in transform)
        {
            //child.GetComponent<Spawn>().SpawnItem();
            child.GetComponent<Spawn>().SpawnItem();
            GameObject.Destroy(child.gameObject);
        }

        //obj.valid = false;

    }

}
