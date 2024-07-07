using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGunShoot;
    public float FireCooldown;

    public bool Automatic;

    private float CurrentCooldown;

    public Animator Gunanim;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCooldown = FireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Automatic)
        {
            if (Input.GetMouseButton(0))
            {
                
                OnGunShoot?.Invoke();
                CurrentCooldown = FireCooldown;

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CurrentCooldown <= 0f)
                {
                    Gunanim.SetTrigger("Shoot");
                    Debug.Log("shoot");CurrentCooldown = 0f;
                    OnGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
            CurrentCooldown -= FireCooldown;
        }
    }
}
