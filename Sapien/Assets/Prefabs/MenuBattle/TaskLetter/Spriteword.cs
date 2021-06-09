using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spriteword : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite SpriteImage;
    public Sprite SpriteTask;
    public string TextTask; 
    public Image BackLetter, OneTask;
    public Text textTaskOneWord;
    public void GenerateTask()
    {
        textTaskOneWord.text = "";
        BackLetter.sprite = SpriteImage;
        OneTask.sprite = SpriteTask;
        
    }
}

