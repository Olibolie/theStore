using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerPickUp : MonoBehaviour
{

    public Transform player;
    public PlayerMovement playerMovement;
    public Animator SpeedAnim;
    private AudioSource audioSource;

    public PowerUps SelectedPowerUp;

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
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == player.name)
        {
            switch (SelectedPowerUp)
            {
                case PowerUps.Speed:
                    audioSource.Play();
                    SpeedAnim.SetTrigger("Drink");
                    Invoke(nameof(SpeedUp), 2f);
                    break;
                case PowerUps.Health:
                    break;
            }

            gameObject.GetComponent<MeshRenderer>().enabled=false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    private void SpeedUp()
    {
        //Debug.Log(playerMovement.moveSpeed);
        playerMovement.moveSpeed = playerMovement.moveSpeed * 2f;

        //Debug.Log(playerMovement.moveSpeed);
        Invoke(nameof(SpeedDown), 2f);
    }

    private void SpeedDown()
    {
        playerMovement.moveSpeed = playerMovement.moveSpeed / 2f;
        Invoke(nameof(PowerReset), 2f);
    }

    private void PowerReset()
    {
        //Debug.Log(playerMovement.moveSpeed);
        Destroy(gameObject);
    }

}



public enum PowerUps
{
    Speed, Health
}