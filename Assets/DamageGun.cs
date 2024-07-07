using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageGun : MonoBehaviour
{

    public float Damage;
    public float BulletRange;
    private Transform PlayerCamera;


    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    public void Shoot()
    {
        //Debug.Log("shoot function called");
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit, BulletRange))
        {
           
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.TryGetComponent(out EnemyAi enemyAi))
            {
                //Debug.Log("enemy take damage");
                enemyAi.TakeDamage(Damage);

            }
        }
    }
}

