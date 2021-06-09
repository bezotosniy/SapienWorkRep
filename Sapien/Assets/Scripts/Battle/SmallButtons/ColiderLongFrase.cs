using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColiderLongFrase : MonoBehaviour
{
    // Start is called before the first frame update
    public string Answer = null;
    bool changePlase;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LongWord")
        {
            changePlase = true;
           StartCoroutine(moving(other.gameObject));
        }
    }
    IEnumerator moving(GameObject obj)
    {
        if (changePlase)
        {
            while (obj.transform.position!= transform.position)
            {
                obj.transform.position = Vector3.Lerp(obj.transform.position, transform.position, 30 * Time.deltaTime);
                if (Vector3.Distance(obj.transform.position, transform.position) < 1)
                {
                    Answer = obj.GetComponentInChildren<Text>().text;
                    Debug.Log(Answer);
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LongWord")
        { changePlase = false; Answer = null; }
        }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
