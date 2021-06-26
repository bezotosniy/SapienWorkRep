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
    
    public int ActiveQuests = 0;
    public int CurrentQuestType;

    public bool NotOpened = true;
    public GameObject CameraPanel;

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
        GetComponent<Button>().interactable = true;
        GetComponent<QuestPanelManager>().AddQuestToActiveList("System", QuestType[CurrentQuestType]);
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
                StartCoroutine(Notifying());
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
        GetComponent<QuestPanelManager>().QuestAdded = true;
    }

    public IEnumerator Notifying()
    {
        yield return new WaitForSeconds(5f);
        if (NotOpened)
        {
            anim.Play("NotificationOpen0");
        }
    }

    public void OnNotificationClick()
    {
        NotOpened = false;
        anim.Play("NotificationOpen0");
        anim.Play("Messages");
        GetComponent<Button>().interactable = false;
        closeable = true;
        if (ActiveQuests < 3)
        {
            ActiveQuests++;
        }
        ActiveQuests++;
    }

    public void OnNotificationPointerDown()
    {
        StartMousePos = Input.mousePosition;
    }

    public void OnNotificationPointerUp()
    {
        EndMousePos = Input.mousePosition;
        if (EndMousePos.x > StartMousePos.x && closeable)
        {
            GetComponent<QuestPanelManager>().QuestAdded = false;
            anim.Play("NotificationOpen0");
        }
    }

    public void OnClickCameraOpen()
    {
        CameraPanel.SetActive(true);
        anim.Play("CameraOpen");
        GetComponent<Button>().interactable = false;
        StartCoroutine(PhoneCloser());
        CameraPanel.GetComponent<CameraManager>().enabled = true;
    }

    public void OnClickCameraClose()
    {
        anim.Play("CameraOpen0");
        GetComponent<Button>().interactable = true;
        CameraPanel.GetComponent<CameraManager>().enabled = false;
        Camera.main.GetComponent<Transform>().localEulerAngles = new Vector3(0f, 0f, 0f);
    }
}
