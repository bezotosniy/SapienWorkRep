using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeManager : MonoBehaviour
{
    public Sprite[] Episode1;
    public Sprite[] Episode2;
    public Sprite[] Episode3;
    public Sprite[] Episode4;
    public Sprite[] Episode5;
    public Animator[] anim;
    
    public void OnPointerEnter(string name)
    {
        switch (name)
        {
            case "Episode1":
                anim[0].Play("Episode1Enter");
                break;
            case "Episode2":
                anim[1].Play("Episode2Enter"); 
                break;
            case "Episode3":
                anim[2].Play("Episode3Enter"); 
                break;
            case "Episode4":
                anim[3].Play("Episode4Enter");
                break;
            case "Episode5":
                anim[4].Play("Episode5Enter"); 
                break;
        }
        
    }

    public void OnPointerExit(string name)
    {
        switch (name)
        {
            case "Episode1":
                anim[0].Play("Episode1Enter0");
                break;
            case "Episode2":
                anim[1].Play("Episode2Enter0");
                break;
            case "Episode3":
                anim[2].Play("Episode3Enter0");
                break;
            case "Episode4":
                anim[3].Play("Episode4Enter0"); 
                break;
            case "Episode5":
                anim[4].Play("Episode5Enter0");
                break;
        }

    }
}
