using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanelManager : MonoBehaviour
{
    public GameObject[] QuestIcon;
    public GameObject[] QuestPrice;
    public GameObject[] QuestDescription;
    public Animator[] anim;
    public GameObject[] FadePanel;
    public int LastActiveQuest;
    public GameObject Warning;
    public GameObject[] QuestDecline;

    public bool[] isHidden = new bool[3];
    public bool QuestAdded = false;

    public void AddQuestToActiveList(string name, string tag)
    {
        if (QuestAdded) {
            if (LastActiveQuest < 3)
            {
                QuestIcon[LastActiveQuest].SetActive(true);
                QuestPrice[LastActiveQuest].GetComponent<Text>().text = Random.Range(100, 1000).ToString();
                QuestDescription[LastActiveQuest].GetComponent<Text>().text = tag;
                LastActiveQuest++;
            }
            else if (LastActiveQuest == 3)
            {
                StartCoroutine(WarningPanel());
            }
            QuestAdded = false;
        }
    }

    IEnumerator WarningPanel()
    {
        Warning.SetActive(true);
        yield return new WaitForSeconds(3f);
        Warning.SetActive(false);
    }

    public void OnClickQuestIconDecline(int n)
    {
        switch (n)
        {
            case 0:
                QuestDescription[0].GetComponent<Text>().text = QuestDescription[1].GetComponent<Text>().text;
                QuestDescription[1].GetComponent<Text>().text = QuestDescription[2].GetComponent<Text>().text;
                QuestPrice[0].GetComponent<Text>().text = QuestPrice[1].GetComponent<Text>().text;
                QuestPrice[1].GetComponent<Text>().text = QuestPrice[2].GetComponent<Text>().text;
                break;
            case 1:
                QuestPrice[1].GetComponent<Text>().text = QuestPrice[2].GetComponent<Text>().text;
                QuestDescription[1].GetComponent<Text>().text = QuestDescription[2].GetComponent<Text>().text;
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
        LastActiveQuest--;
    }

    public void OnClickQuestIconDecline(string tag)
    {
        int n;
        if (GameObject.FindWithTag(tag).GetComponent<Transform>().parent == QuestIcon[0])
        {
            n = 0;
        }
        else if (GameObject.FindWithTag(tag).GetComponent<Transform>().parent == QuestIcon[1])
        {
            n = 1;
        }
        else if (GameObject.FindWithTag(tag).GetComponent<Transform>().parent == QuestIcon[2])
        {
            n = 2;
        }
        else { n = 10; }
        switch (n)
        {
            case 0:
                QuestDescription[0].GetComponent<Text>().text = QuestDescription[1].GetComponent<Text>().text;
                QuestDescription[1].GetComponent<Text>().text = QuestDescription[2].GetComponent<Text>().text;
                QuestPrice[0].GetComponent<Text>().text = QuestPrice[1].GetComponent<Text>().text;
                QuestPrice[1].GetComponent<Text>().text = QuestPrice[2].GetComponent<Text>().text;
                break;
            case 1:
                QuestPrice[1].GetComponent<Text>().text = QuestPrice[2].GetComponent<Text>().text;
                QuestDescription[1].GetComponent<Text>().text = QuestDescription[2].GetComponent<Text>().text;
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
        LastActiveQuest--;
    }

    public void OnClickHidePanel(int n)
    {
        if (!isHidden[n])
        {
            anim[n].Play("QuestFade");
            QuestDescription[n].SetActive(false);
            QuestPrice[n].SetActive(false);
            QuestDecline[n].SetActive(false);
            isHidden[n] = true;
        }
    }

    public void OnClickShowPanel(int n)
    {
        if (isHidden[n])
        {
            anim[n].Play("QuestFade0");
            QuestDescription[n].SetActive(true);
            QuestPrice[n].SetActive(true);
            QuestDecline[n].SetActive(true);
            isHidden[n] = false;
        }
    }
}
