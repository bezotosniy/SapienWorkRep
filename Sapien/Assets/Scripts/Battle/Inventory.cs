using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CartoonFX;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class Inventory : MonoBehaviour
{
    KeyBordController keyBoard;
 
    [Header("Person")]
    public MovingBattlePeron person;
    public Animator animPerson;
    public Slider hpPersonSlider;
    public Text TextPersonHP;
    public int HP_Person;
    int Start_hp_Person;
    [Space]
    public string[] TaskString;
    private Animator CanvasAnim;
    [Space]
    [Header("Gems")]
    public Animator gems;
    public GameObject[] gem;
    public GameObject ParticleCrashGem;
    public Transform tranfGem;
    public GameObject ParticleUronNumber;
    public Button ClickOnGem;
    [Space]
    [Header("Enemy")]
    public EnemyController EnemyControll;

    int indexGem = 0;
    int CurrentUron;

    [Space]
    [Header("Task")]
    public VoiceRecognision voiceRec;
    public GameObject OneWord,OneFraze;
    public Text TimeText;
    bool TimeGo;
    [Space]
    private Camera Cam;
    private Ray RayMouse;
    void Start()
    {

        Start_hp_Person = HP_Person;
        HP_Person_Controller(0);
        ClickOnGem.onClick.AddListener(ClickOnGems);
        InitialiseFrase();
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        keyBoard = GetComponent<KeyBordController>();
       
        CanvasAnim = GetComponent<Animator>();
        StartCoroutine(StartFight());
        CreateRandom();

    }
    void CreateRandom()
    {
        for (int i = RandomTask.Length - 1; i > 0; i--)
        {
            var r = new System.Random();
            int j = r.Next(i);
            var t = RandomTask[i];
            RandomTask[i] = RandomTask[j];
            RandomTask[j] = t;
        }
    }
    void Update()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
            if (Cam != null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = Cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    if (hit.collider.tag == "Enemy")
                    {   
                        StartCoroutine(person.ClickOnEnemy(hit.collider.gameObject.transform.position));
                    }
                }
            }
            else { Debug.Log("No camera"); }
        }
    }
        
    void InitialiseFrase()
    {
        PlayerPrefs.SetString("phrase" + 0, "How are you?");
        PlayerPrefs.SetString("phrase" + 1, "Hello there");
        PlayerPrefs.SetString("phrase" + 2, "Nice to meet you");
        PlayerPrefs.SetString("phrase" + 3, "Good morning");
   
        int i = 0;
        string[] a;
        while(PlayerPrefs.GetString("phrase" + i) != "")
        {
           TaskString[i] = PlayerPrefs.GetString("phrase" + i);
            PlayerPrefs.SetInt("phraseLength", i);
            i++;
        }
               
    }
  
    public IEnumerator StartFight()
    {
        yield return new WaitForSeconds(17.5f);
        CanvasAnim.SetTrigger("StartFight");
        EnemyControll.MouseBar.gameObject.SetActive(true);
    }
    public void ClickOnGems()
    {
        if (indexGem == 0)
        {
            gems.SetTrigger("Pick");
            StartCoroutine(WaitToStart());
        }
        ClickOnGem.interactable = false;
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(WaitForChangeScale());
        
    }
    IEnumerator WaitForChangeScale()
    {
        if (indexGem <= 3)
        {
            float i = gem[indexGem].transform.localScale.x;
            for (float q = i; q < i * 2; q += .1f)
            {
                gem[indexGem].transform.localScale = new Vector3(q, q, q);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1);
            CanvasAnim.SetTrigger("NewTask");
            TimeGo = true;
        StartCoroutine(InstantTask());
            StartCoroutine(Time());
            indexGem++;
        }
    }
    public IEnumerator Time()
    { int allTime = 0;
        int second, minutes;
        while (TimeGo) {
            yield return new WaitForSeconds(1f);
            second = allTime % 60;
            minutes = allTime / 60;
            if(second < 10)
                TimeText.text = minutes + ":" + "0"+second;
            else
                TimeText.text = minutes + ":" + second;
            allTime++;
            
        }
        yield break;
    }
    public int[] RandomTask;
    int num = 0;
    IEnumerator InstantTask()
    {
      
        yield return new WaitForSeconds(1.5f);
        if (num < RandomTask.Length)
        {
            if (RandomTask[num] == 0) { OneWord.SetActive(true); keyBoard.InstWord(); }
            else if (RandomTask[num] == 1) { keyBoard.KeyBoard(); }
            else { voiceRec.gameObject.SetActive(true); voiceRec.changeText(TaskString[Random.Range(0, TaskString.Length)]); }
            num++;
        }
        else
        {
            voiceRec.gameObject.SetActive(true); voiceRec.changeText(TaskString[Random.Range(0, TaskString.Length)]);
        }
            }
    public void EnemyDie()
    {
        Instantiate(ParticleUronNumber, EnemyControll.transform.position + new Vector3(1,1,1),Quaternion.identity);
        ParticleUronNumber.GetComponent<CFXR_ParticleText_Runtime>().text = CurrentUron.ToString();
        EnemyControll.hpSlider.value -= CurrentUron;
        if (EnemyControll.hpSlider.value <= 0)
        {
            EnemyControll.hpSlider.value = 0;
            EnemyControll.EnemyAnim.SetTrigger("die");
        }
        else
        {
            StartCoroutine(EnemyControll.EnemyGiveUron(45));
        }

    }
    public void HP_Person_Controller(int damage)
    {
        hpPersonSlider.maxValue = Start_hp_Person;
        HP_Person -= damage;
        TextPersonHP.text = HP_Person.ToString() + "/" + Start_hp_Person;
        hpPersonSlider.value = HP_Person;
    }
 
    public void CrashGem(int Plus)
    {
        CurrentUron += Plus;

        StartCoroutine(CrashGemCoroutine());
    }
    IEnumerator CrashGemCoroutine()
    {
        yield return new WaitForSeconds(2);
        CanvasAnim.SetTrigger("NewTask");
        TimeGo = false;
        
        yield return new WaitForSeconds(0.5f);
    OneWord.SetActive(false); OneFraze.SetActive(false); voiceRec.gameObject.SetActive(false);
    yield return new WaitForSeconds(1f);
    tranfGem.position = gem[indexGem - 1].transform.position;
        gem[indexGem - 1].SetActive(false);
        Instantiate(ParticleCrashGem, tranfGem.transform);

        StartCoroutine(WaitForChangeScale());

    }


}

