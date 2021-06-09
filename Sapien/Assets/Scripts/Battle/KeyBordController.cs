
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class KeyBordController : MonoBehaviour
{
    Inventory inv;
    string textTask;

    public Transform position;
    int ID;
    string[] keyLength;

    [Space]
    [Header("Char")]
    public Transform pointLetterInst;
    public Spriteword[] LetterList;  
    public Button[] buttonLetter;
    public Button space, remove;
    GameObject instWord;
    [Space]
    [Header("Phrase")]
    [Range(1, 400)]
    public int OffsetFraseX;
    public string TextOneButton;
    public GameObject movingObject;
   
    public GameObject longFrase;
    public Button EnterText;
    public ColiderLongFrase[] Cl;
    public string TaskSum;
    private void Start()
    {
        EnterText.onClick.AddListener(PostTaskAnswer);
        position.gameObject.SetActive(false);
        inv = GetComponent<Inventory>();
    }
    public void KeyBoard()
    {
        int Lenth = PlayerPrefs.GetInt("phraseLength");
        if (Lenth > 5)
            textTask = PlayerPrefs.GetString("phrase" + Random.Range(Lenth - 5, Lenth));
        else
            textTask = PlayerPrefs.GetString("phrase" + Random.Range(0, Lenth));
        
       
        InstFrase();
      
    }

    public void InstWord()
        
    {   if (LetterList.Length > 1)
        {
            ID = Random.Range(0, LetterList.Length);
        }
        else ID = 0;
        instWord = Instantiate(LetterList[ID].gameObject, transform);
        LetterList[ID].GenerateTask();
        textTaskChar = "";
        LetterList[ID].textTaskOneWord.text = textTaskChar;
        normalChar = false;
        int i = 0;
        int random = buttonLetter.Length - 1;
        for (; i < buttonLetter.Length; i++)
        {
            if (Random.Range(i, buttonLetter.Length) == random && !normalChar)
            {
                normalChar = true;
                buttonLetter[i].GetComponentInChildren<Text>().text = LetterList[ID].TextTask[numberLetter].ToString();
            }
            else
            {
                System.Random rand = new System.Random();

                char ch = (char)rand.Next(0x0061, 0x007A);
                buttonLetter[i].GetComponentInChildren<Text>().text = ch.ToString();
            }
        }
        numberLetter++;
    }
    int numberLetter = 0;
    bool normalChar;
    string textTaskChar;
    public void LetterInButton(string text)
    {
        textTaskChar= LetterList[ID].textTaskOneWord.text + text;
        LetterList[ID].textTaskOneWord.text = textTaskChar;
       
        if (numberLetter < LetterList[ID].TextTask.Length)
        {normalChar = false;
            int i = 0;
            int random = buttonLetter.Length - 1;
            for (; i < buttonLetter.Length; i++)
            {
                if (Random.Range(i,buttonLetter.Length) == random &&!normalChar)
                {
                    normalChar = true;
                    buttonLetter[i].GetComponentInChildren<Text>().text = LetterList[ID].TextTask[numberLetter].ToString();
                }
                else
                {
                    System.Random rand = new System.Random();
                  
                    char ch = (char)rand.Next(0x0061, 0x007A);
                    buttonLetter[i].GetComponentInChildren<Text>().text = ch.ToString();
                }       
            }
            
            
            numberLetter++;
        }
        else
        {
            numberLetter = 0;
            Destroy(instWord);
            if (LetterList[ID].textTaskOneWord.text == LetterList[ID].TextTask)
                inv.CrashGem(45);
            else
                InstWord();
        }
    }
    void InstFrase()
    {
        int[] rand;
        
        Debug.Log("One");
        position.gameObject.SetActive(true);
        GameObject obj;
        keyLength = textTask.Split(' ');
        var r = new System.Random();
        for (int i = keyLength.Length - 1; i > 0; i--)
        {
            int j = r.Next(i);
            var t = keyLength[i];
            keyLength[i] = keyLength[j];
            keyLength[j] = t;
        }
        for (int i = 0; i < keyLength.Length; i++)
        {
            Debug.Log(keyLength[i]);
            Vector3 pos = new Vector3(position.position.x + i * OffsetFraseX, position.position.y, position.position.z);
            obj = Instantiate(longFrase, pos, Quaternion.identity);
            obj.transform.SetParent(position);
            obj.GetComponentInChildren<Text>().text = keyLength[i];
        }

    }

    void MoveObject()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = GetComponent<RectTransform>().position.z;
        movingObject.transform.position = pos;
    }
   
    public void PostTaskAnswer()
    {
        TaskSum = "";
        for(int i = 0; i<Cl.Length;i++)
        {
            if (Cl[i].Answer != null)
            {
                TaskSum += " ";
                TaskSum += Cl[i].Answer;
            }
        }
     

        if(System.String.Compare(TaskSum, textTask, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreSymbols) == 0)
        {
            inv.CrashGem(45);
            var obj = FindObjectsOfType<ButtonFrase>();
            foreach(ButtonFrase i in obj){
                Destroy(i.gameObject);
            }
        }
        else
        {
            inv.CrashGem(0);
            var obj = FindObjectsOfType<ButtonFrase>();
            foreach (ButtonFrase i in obj)
            {
                Destroy(i.gameObject);
            }
        }
        position.gameObject.SetActive(false);
    }
    public bool canPressed;
    private void Update()
    {
        if (Input.GetMouseButton(0) && canPressed)
        {
            MoveObject();
        }
    }
}
