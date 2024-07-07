using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteLook : MonoBehaviour
{

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerCamera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
