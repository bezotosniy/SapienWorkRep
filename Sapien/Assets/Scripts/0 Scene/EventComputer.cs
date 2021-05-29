using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventComputer : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
   public void InteractiveOn() { anim.SetBool("Interactive", true); }
    public void InteractiveOff() { anim.SetBool("Interactive", false); }
   
    // Update is called once per frame
    void Update()
    {
       
    }
}
