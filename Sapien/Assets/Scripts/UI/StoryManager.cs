using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{

    public GameObject SeasonPanel;
    public int SeasonNumber;
    public GameObject MainButton;
    public float Increment;
    public Sprite[] SeasonSprite;
    public GameObject[] Season;
    public int MaxSeasonAvailable;

    public void OnClickSeasonOpener(int season)
    {
        SeasonPanel.SetActive(true);
        if (SeasonNumber != null && SeasonNumber != 0)
        {
            if (SeasonNumber != season)
            {
                OnPointerEnterSeason("Season" + season);
                OnPointerExitSeason("Season" + SeasonNumber);
                SeasonNumber = season;
            }
        }
        else
        {
            OnPointerEnterSeason("Season" + season);
            SeasonNumber = season;
        }
    }

    public void OnPointerEnterIncrease(string tag)
    {
        if (tag == "Season" + SeasonNumber)
        {
            
        }
        else
        {
            if ((int)System.Char.GetNumericValue(tag[6]) <= MaxSeasonAvailable)
            {
                GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(Increment, Increment, 1f);
            }
            else
            {

            }
        }
    }

    public void OnPointerEnterDecrease(string tag)
    {
        if (("Season" + SeasonNumber) != tag)
        {
            GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnPointerEnterSeason(string tag)
    {
        GameObject.Find(tag).GetComponent<Image>().sprite = SeasonSprite[1];
        GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.35f, 1);
    }

    public void OnPointerExitSeason(string tag)
    {
        GameObject.Find(tag).GetComponent<Image>().sprite = SeasonSprite[0];
        GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
    }
}
