using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFrase : MonoBehaviour
{
    // Start is called before the first frame update
    KeyBordController kb;
    
    void Start()
    {
        kb = FindObjectOfType<KeyBordController>();
    }
    public void ClickOnObj()
    {
        kb.movingObject = gameObject;
        kb.canPressed = true;
    }
    public void RemoveClick()
    {
        kb.canPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
