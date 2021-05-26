using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public GameObject canvas;

    [Range(0, 2)]
    public float speed;
    public Animator anim;
    public void Enter() { anim.SetTrigger("EnterIn"); }
    public void EnterBack() { anim.SetTrigger("BackEnter"); }
    public void next1() { anim.SetTrigger("next1"); }
    public void Backnext1() { anim.SetTrigger("Backnext1"); }
}
