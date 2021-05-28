using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartMenuController : MonoBehaviour
{
    public GameObject canvas, boy,girl;
    public Color blue, prpl, gray;
    public Image MaleBar;
    public Text genderExeption;
    bool isClicked = false;
    public Button next;
    int nextNum;
    [Range(0, 2)]
    public float speed;
    public Animator anim;
    public void click()
    {
        isClicked = true;
        

    }
    public void Enter() { anim.SetTrigger("EnterIn"); }
    public void EnterBack() { anim.SetTrigger("BackEnter"); }
    public void next1() { anim.SetTrigger("next1"); }
    public void Backnext1() { anim.SetTrigger("Backnext1"); girl.SetActive(false); boy.SetActive(false); }
    public void ChoiceBoy() { anim.SetBool("Boy", true ); }
    public void UNchoiceBoy() { anim.SetBool("Boy", false ); }
    public void ChoiceGirl() { anim.SetBool("Girl", true); }
    public void UNchoiceGirl() { anim.SetBool("Girl", false); }
    public void MakeChoiceGirl() { anim.SetTrigger("ChangeMale"); MaleBar.color = prpl; PlayerPrefs.SetString("gender", "female"); genderExeption.text = "Вы уверены, что хотите выбрать женский пол?"; }
    public void MakeChoiceBoy() { anim.SetTrigger("ChangeMale"); MaleBar.color = blue; PlayerPrefs.SetString("gender", "male"); genderExeption.text = "Вы уверены, что хотите выбрать мужской пол?"; }
    public void YesGender()
    {
        anim.SetTrigger("ChangeMale");
        if (PlayerPrefs.GetString("gender") == "female") { girl.SetActive(true); boy.SetActive(false); }
        if (PlayerPrefs.GetString("gender") == "male") { boy.SetActive(true); girl.SetActive(false); }

    }
    public void easyON() { anim.SetBool("easy", true); } public void easyOFF() { anim.SetBool("easy", false); } 
    public void normalON() { anim.SetBool("normal", true); } public void normalOFF() { anim.SetBool("normal", false); } 
    public void hardON() { anim.SetBool("hard", true); } public void hardOFF() { anim.SetBool("hard", false); } 
    

}
