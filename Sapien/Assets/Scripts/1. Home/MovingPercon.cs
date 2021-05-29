using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPercon : MonoBehaviour
{
    CapsuleCollider coll;
    Rigidbody rb;

    [Range(0, 10)]
    public float speed;
    [Range(0, 100)]
    public float speedRot;
    GameObject main;
    // Start is called before the first frame update 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        main = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Moving(float x,float z)
    {
        Vector3 m_Input = (main.transform.forward + new Vector3(x, 0, z));
        rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
      
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)){ Moving(-1, 0);}
        if (Input.GetKey(KeyCode.A)){ Moving(0, -1);}
        if (Input.GetKey(KeyCode.S)){ Moving(1, 0);}
        if (Input.GetKey(KeyCode.D)){ Moving(0, 1);}
        
    }
}
