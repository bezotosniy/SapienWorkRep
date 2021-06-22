using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesManager : MonoBehaviour
{
    public GameObject ChatIcon;
    public GameObject ChatPanel;
    public GameObject QuestText;
    public Vector3 FirstChatPos = new Vector3(0f, 425f, 0f);
    public bool QuestAvailable = false;
    public float Increment = 130f;
    public GameObject chat;
    public GameObject Content;
    public GameObject[] Quests;
    public int n = 0;
    public int OpenedQuest = 100;

    public void OnClickChatOpener(GameObject target)
    {
        ChatIcon.SetActive(false);
        ChatPanel.SetActive(true);
        QuestText.GetComponent<Text>().text = target.tag;
        OpenedQuest = (int)System.Char.GetNumericValue(target.name[0]);
    }

    public void OnClickChatDelete()
    {
        ChatIcon.SetActive(true);
        ChatPanel.SetActive(false);
        GameObject.Find("PhoneButton").GetComponent<QuestPanelManager>().OnClickQuestIconDecline(Quests[OpenedQuest].tag);
        Quests[OpenedQuest].SetActive(false);
        Destroy(Quests[OpenedQuest]);
        for (int k = OpenedQuest; k >= 0; k--)
        {
            if (Quests[k] != null)
            {
                Vector3 CurrentPos = Quests[k].GetComponent<RectTransform>().position;
                Quests[k].GetComponent<RectTransform>().position = new Vector3(CurrentPos.x, CurrentPos.y + Increment, CurrentPos.z);
            }
        }
        OpenedQuest = 100;
    }

    public void AddQuest(string name, string type)
    {
        for (int k = 0; k <= n; k++)
        {
            if (Quests[k] != null)
            {
                Vector3 CurrentPos = Quests[k].GetComponent<RectTransform>().position;
                Quests[k].GetComponent<RectTransform>().position = new Vector3(CurrentPos.x, CurrentPos.y - Increment, CurrentPos.z);
            }
        }

        Quests[n] = GameObject.Instantiate(chat, FirstChatPos, Quaternion.identity) as GameObject;
        GameObject.Find("Chat1(Clone)/Name").GetComponent<Text>().text = name;
        GameObject.Find("Chat1(Clone)/Type").GetComponent<Text>().text = type;
        Quests[n].name = n.ToString() + name;
        Quests[n].tag = type;
        Quests[n].GetComponent<RectTransform>().SetParent(Content.GetComponent<RectTransform>(), false);
        OnClickChatOpener(Quests[n]);
        n++;
    }

}
