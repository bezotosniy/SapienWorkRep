using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    Vector3 vector;
    [Range(0.1f, 10)]
    public float speed;
    public int HP;
  
    void Start()
    {
        vector = canvas.transform.position - transform.position;
    }

    void ChangeHP(int changeCount)
    {
        HP += changeCount;
    }
    // Update is called once per frame
    void Update()
    {
        canvas.transform.position = Vector3.Lerp(canvas.transform.position, transform.position + vector, speed * Time.deltaTime);
    }
}
