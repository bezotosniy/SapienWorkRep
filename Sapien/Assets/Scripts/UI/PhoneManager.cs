using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
    public GameObject Phone;
    public float Increment;
    public GameObject StoryScreen;
    public Animator anim;

    public void OnButtonClickPhoneOpener()
    {
        Phone.SetActive(true);
    }

    public void OnButtonClickPhoneCloser()
    {
        StartCoroutine(PhoneCloser());
    }

    IEnumerator PhoneCloser()
    {
        Phone.GetComponent<Animator>().Play("PhoneClosed");
        yield return new WaitForSeconds(1f);
        Phone.SetActive(false);
    }

    public void OnPointerEnterIncrease(string tag)
    {
        GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(Increment, Increment, 1f);
    }

    public void OnPointerEnterDecrease(string tag)
    {
        GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 1f);
    }

    public void OnPointerClickStoryIcon(string tag)
    {
        StartCoroutine(PhoneCloser());
        anim.Play("StoryScreen");
        GetComponent<Button>().interactable = false;
    }

    public void OnPointerClickStoryIconClose(string tag)
    {
        anim.Play("StoryScreen0");
        Phone.SetActive(true);
        GetComponent<Button>().interactable = true;
    }

    public void OnPointerClickCardIcon(string tag)
    {
        StartCoroutine(PhoneCloser());
        anim.Play("FragmentCard");
        GetComponent<Button>().interactable = false;
    }

    public void OnPointerClickCardIconClose(string tag)
    {
        anim.Play("FragmentCard0");
        Phone.SetActive(true);
        GetComponent<Button>().interactable = true;
    }



}
