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
        if (season <= MaxSeasonAvailable)
        {
            SeasonPanel.SetActive(true);
            if (SeasonNumber != null && SeasonNumber != 0)
            {
                OnPointerExitSeason("Season" + SeasonNumber);
                OnPointerEnterSeason("Season" + season);
                SeasonNumber = season;
            }
            else
            {
                OnPointerEnterSeason("Season" + season);
                SeasonNumber = season;
            }
        }
    }

    public void OnPointerEnterIncrease(string tag)
    {
        if (tag == "Season" + SeasonNumber)
        {
            
        }
        else if (tag.Contains("Season"))
        {
            if ((int)System.Char.GetNumericValue(tag[6]) <= MaxSeasonAvailable)
            {
                GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(Increment, Increment, 1f);
            }
        }
        else if (tag.Contains("Day"))
        {
            GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(Increment, Increment, 1f);
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
        GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(1.125f*Increment, 1.2f*Increment, 1);
    }

    public void OnPointerExitSeason(string tag)
    {
        GameObject.Find(tag).GetComponent<Image>().sprite = SeasonSprite[0];
        GameObject.Find(tag).GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
    }
}
