using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuControllerNoReg : MonoBehaviour
{
    /*public GameObject canvas, boy, girl, GenderConfirm, back, back2, MicTestPanel;
    public Color gray;
    public Sprite blue, prpl;
    public Image MaleBar;
    public Text genderAcception;
    bool isClicked = false;
    public GameObject next, nextNorm, next2;*/

    public Animator TaxiAnim;
    public GameObject FadePanel;
    public GameObject boy, girl;
    public Animator anim;

    /*private void Start()
    {
        next2.SetActive(false);
        girl.SetActive(false); boy.SetActive(false);
        next.SetActive(true);
        nextNorm.SetActive(false);
    }*/
    /*public void click(Image but)
    {
        but.color = gray;
        isClicked = true;
        next.SetActive(false);
        nextNorm.SetActive(true);

    }*/
    /*public void Right() 
    { 
        anim.SetTrigger("TurnRight"); 
    }

    public void Left() 
    { 
        anim.SetTrigger("TurnLeft"); 
    }*/
    public void Enter() 
    { 
        anim.SetTrigger("EnterIn");
        if (PlayerPrefs.GetString("gender") == "female"){
            boy.SetActive(false);
        }
        else if (PlayerPrefs.GetString("gender") == "male")
        {
            girl.SetActive(false);
        }
    }
    public void EnterBack() 
    { 
        anim.SetTrigger("BackEnter"); 
    }
    /*public void next1() 
    { 
        anim.SetTrigger("next1"); 
    }
    public void Backnext1() 
    {
        anim.SetTrigger("Backnext1");
        girl.SetActive(false);
        boy.SetActive(false);
    }
    public void BackToGender()
    {
        Debug.Log(NameIsChosen);
        switch (NameIsChosen)
        {
            case false:
                anim.Play("idle3");
                break;
            case true:
                anim.SetTrigger("BackToName");
                NameIsChosen = false;
                break;
        }
    }*/
    /*public void ChoiceBoy() 
    { 
        anim.SetBool("Boy", true); 
    }
    public void UNchoiceBoy() 
    { 
        anim.SetBool("Boy", false); 
    }
    public void ChoiceGirl() 
    { 
        anim.SetBool("Girl", true); 
    }
    public void UNchoiceGirl() 
    { 
        anim.SetBool("Girl", false); 
    }
    public void MakeChoiceGirl() 
    { 
        anim.SetTrigger("ChangeMale"); 
        MaleBar.sprite = prpl; 
        PlayerPrefs.SetString("gender", "female"); 
        genderAcception.text = "Вы уверены, что хотите выбрать женский пол?"; 
    }
    public void MakeChoiceBoy() 
    { 
        anim.SetTrigger("ChangeMale"); 
        MaleBar.sprite = blue; 
        PlayerPrefs.SetString("gender", "male"); 
        genderAcception.text = "Вы уверены, что хотите выбрать мужской пол?"; 
    }
    public void easyON() 
    { 
        anim.SetBool("easy", true); 
    } 
    public void easyOFF() 
    { 
        anim.SetBool("easy", false); 
    }
    public void normalON() 
    { 
        anim.SetBool("normal", true); 
    } 
    public void normalOFF() 
    { 
        anim.SetBool("normal", false); 
    }
    public void hardON() 
    { 
        anim.SetBool("hard", true); 
    } 
    public void hardOFF() 
    { 
        anim.SetBool("hard", false); 
    }
    public void ChoiceName() 
    {
        NameIsChosen = true;
        anim.SetBool("ChousenName", true);
    }
    public void ChoiceNameBack() 
    {
        anim.SetBool("ChousenName", false);
    }
    public void Accept()
    {
        Debug.Log(PlayerPrefs.GetString("name"));
        ChoiceNameBack();
        next2.SetActive(true);
        textName.text = PlayerPrefs.GetString("name");
    }
    public void YesGender()
    {
        back.SetActive(false);
        back2.SetActive(true);
        GenderConfirm.SetActive(false);
        anim.SetTrigger("ChangeMale");
        if (PlayerPrefs.GetString("gender") == "female") { 
            girl.SetActive(true); boy.SetActive(false); 
        }
        if (PlayerPrefs.GetString("gender") == "male") { 
            boy.SetActive(true); girl.SetActive(false); 
        }
    }*/
    public void OnClickTV()
    {
        Debug.Log("TV Clicked");
        anim.Play("MicTest");
    }
    public void OnClickExitTV()
    {
        StartCoroutine(TaxiDriveAvay());
        Debug.Log("MicTestExited");
        anim.Play("MicTest0");
    }

    IEnumerator TaxiDriveAvay()
    {
        yield return new WaitForSeconds(1f);
        TaxiAnim.Play("TaxiArrival");
        yield return new WaitForSeconds(2f);
        GameObject.FindWithTag("Player").SetActive(false);
        yield return new WaitForSeconds(1.5f);
        FadePanel.SetActive(true);
    }
}
