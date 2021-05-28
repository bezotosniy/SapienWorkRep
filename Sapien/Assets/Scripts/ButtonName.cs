using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonName : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource AS;
    GameObject textObj;
    public Color Gray;
    ButtonName[] AllObj;
    
    void Start()
    {
        textObj = transform.Find("Text").gameObject;
        //Debug.Log(textObj.GetComponent<Text>().text);
        AS = GetComponent<AudioSource>();

    }
    public void ClickName()
    {
        PlayerPrefs.SetString("name", textObj.GetComponent<Text>().text);
       AllObj = FindObjectsOfType<ButtonName>();
        Debug.Log(AllObj.Length);
        foreach(ButtonName name in AllObj)
        {
            if(name.GetComponent<RectTransform>().position == GetComponent<RectTransform>().position)
            {
                name.GetComponent<Image>().color = Gray;
                Debug.Log("MyButton");
               // AS.Play();       // раскоментить, когда будут mp3
            }
            else
            {
                name.GetComponent<Image>().color = Color.white;
             //   Debug.Log("MyButton");
            }
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
