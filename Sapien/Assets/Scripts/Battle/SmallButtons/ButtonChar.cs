using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChar : MonoBehaviour
{
    Text text;
    KeyBordController kb;
    void Start()
    {
        kb = FindObjectOfType<KeyBordController>();
        text = GetComponentInChildren<Text>();
    }
    public void OnClick()
    {
        kb.LetterInButton(text.text);
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
