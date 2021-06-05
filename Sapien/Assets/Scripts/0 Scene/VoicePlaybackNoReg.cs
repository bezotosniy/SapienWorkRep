using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoicePlaybackNoReg : MonoBehaviour
{

    public SpriteRenderer MicSprite;
    public Sprite[] sprite;
    public AudioSource speaker;
    public GameObject speak, wait, PlayBar, PauseBar;

    public bool isPaused = false;
    public bool isRecording = false;
    public bool RecordResult = false;
    public Animator TaxiAnim;

    public Text result;

    void Start()
    {
        StartCoroutine(HelloVoice());
    }

    void Update()
    {
        if (speaker.isPlaying && !isPaused)
        {
            PlayBar.SetActive(false);
            isRecording = false;
            PauseBar.SetActive(true);
            PauseBar.GetComponent<Image>().fillAmount += Time.deltaTime / speaker.clip.length;
            MicSprite.sprite = sprite[0];
            speak.SetActive(false);
            wait.SetActive(true);
            Debug.Log(speaker.clip.length);
        }
        else if (!speaker.isPlaying && !isPaused)
        {
            isPaused = false;
            isRecording = true;
            PauseBar.GetComponent<Image>().fillAmount = 0f;
            PlayBar.SetActive(true);
            PauseBar.SetActive(false);
            MicSprite.sprite = sprite[1];
            speak.SetActive(true);
            wait.SetActive(false);
        }
        
        if (RecordResult == true)
        {
            Debug.Log(RecordResult);
            GameObject.Find("MenuController").GetComponent<StartMenuController>().OnClickExitTV();
        }
    }

    IEnumerator HelloVoice()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Hello");
        speaker.Play(0);
    }

    public void HelloPlayback()
    {
        if (!speaker.isPlaying)
        {
            speaker.Play(0);
            isPaused = false;
        }
        else if (speaker.isPlaying)
        {
            speaker.Pause();
            isPaused = true;
        }
    }

    
}
