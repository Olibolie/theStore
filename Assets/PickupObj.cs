using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickupObj : MonoBehaviour
{

    public Transform player;
    public PlayerMovement playerMovement;
    public TextMeshProUGUI objectiveText;
    public int objectiveNum;
    public string Name;
    public bool IsBought;
    public ObjectivePlayer objectivePlayer;

    


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
        if (playerMovement == null)
        {
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
        if (objectiveText == null)
        {
            objectiveText = GameObject.Find("objectiveText").GetComponent<TextMeshProUGUI>();
        }

        //Debug.Log(objectiveText);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public PickupObj(string name)
    {
        Name = name;
        IsBought = false;
    }
    public void ToggleBought()
    {
        IsBought = !IsBought;
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == player.name)

            objectivePlayer.ToggleItemBought(Name);


            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    

  
    private void RemoveObj()
    { 
        Destroy(gameObject);
    }

}
