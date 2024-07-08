using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHovering : MonoBehaviour
{
    public float height = 2;
    public float speed = 1.5f;
    


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        p.y = 1 + height * Mathf.Cos(Time.time * speed);
        transform.position = p;
    }
}
