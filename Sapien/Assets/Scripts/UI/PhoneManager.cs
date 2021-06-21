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
    public GameObject Notification;
    public bool closeable = false;
    public Vector3 StartMousePos;
    public Vector3 EndMousePos;
    public GameObject Messages;
    private bool QuestAvailable = false;
    public string[] QuestType = { "Start of fragment", "Story quest", "Battle", "Wish mission" };
    public bool SecondQuestAvailable = false;
    public GameObject[] QuestIcon;
    public GameObject[] QuestPrice;
    public GameObject[] QuestDescription;
    public int ActiveQuests = 0;
    public int CurrentQuestType;

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

    public void OnPointerClickMessagesIcon(string tag)
    {
        StartCoroutine(PhoneCloser());
        anim.Play("Messages");
        GetComponent<Button>().interactable = false;
    }

    public void OnPointerClickMessagesIconClose(string tag)
    {
        anim.Play("Messages0");
        Phone.SetActive(true);
        GetComponent<Button>().interactable = true;
        QuestIcon[ActiveQuests].SetActive(true);
        ActiveQuests++;
    }

    public void OnNotificationOpener()
    {
        Notification.SetActive(true);
        Messages.GetComponent<MessagesManager>().QuestAvailable = QuestAvailable;
        if (!QuestAvailable)
        {
            GameObject.Find("QuestType").GetComponent<Text>().text = QuestType[0];
            Messages.GetComponent<MessagesManager>().AddQuest("System", QuestType[0]);
            anim.Play("NotificationOpen");
            /*Messages.GetComponent<MessagesManager>().OnClickChatOpener(QuestType[0]);*/
            QuestAvailable = true;
            CurrentQuestType = 0;
        }
        else
        {
            if (!SecondQuestAvailable)
            {
                GameObject.Find("QuestType").GetComponent<Text>().text = QuestType[1];
                Messages.GetComponent<MessagesManager>().AddQuest("System", QuestType[1]);
                anim.Play("NotificationOpen");
                /*Messages.GetComponent<MessagesManager>().OnClickChatOpener(QuestType[1]);*/
                SecondQuestAvailable = true;
                CurrentQuestType = 1;
            }
            else
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        GameObject.Find("QuestType").GetComponent<Text>().text = QuestType[2];
                        Messages.GetComponent<MessagesManager>().AddQuest("Andrew", QuestType[2]);
                        CurrentQuestType = 2;
                        break;
                    case 1:
                        GameObject.Find("QuestType").GetComponent<Text>().text = QuestType[3];
                        Messages.GetComponent<MessagesManager>().AddQuest("Andrew", QuestType[3]);
                        CurrentQuestType = 3;
                        break;

                }
                anim.Play("NotificationOpen");
            }
        }
        
    }

    public void OnNotificationClick()
    {
        anim.Play("NotificationOpen0");
        anim.Play("Messages");
        GetComponent<Button>().interactable = false;
        closeable = true;
        QuestDescription[ActiveQuests].GetComponent<Text>().text = QuestType[CurrentQuestType];
                
    }

    public void OnNotificationPointerDown()
    {
        StartMousePos = Input.mousePosition;
        Debug.Log("Down");
    }

    public void OnNotificationPointerUp()
    {
        Debug.Log("Up");
        EndMousePos = Input.mousePosition;
        if (EndMousePos.x > StartMousePos.x && closeable)
        {
            anim.Play("NotificationOpen0");
        }
    }

    public void OnClickQuestIconDecline(int n)
    {
        Debug.Log("Closing");
        switch (n)
        {
            case 0:
                QuestDescription[2] = QuestDescription[1];
                QuestDescription[1] = QuestDescription[0];
                QuestPrice[2] = QuestPrice[1];
                QuestPrice[1] = QuestPrice[0];
                Debug.Log("Case 1");
                break;
            case 1:
                QuestPrice[1] = QuestPrice[2];
                QuestDescription[1] = QuestDescription[2];
                break;
            case 2:
                break;
        }
        if (QuestIcon[2].activeSelf)
        {
            QuestIcon[2].SetActive(false);
        }
        else if (QuestIcon[1].activeSelf)
        {
            QuestIcon[1].SetActive(false);
        }
        else
        {
            QuestIcon[0].SetActive(false);
        }
        ActiveQuests--;
    }
}
