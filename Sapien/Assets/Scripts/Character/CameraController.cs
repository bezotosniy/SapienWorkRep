using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Player;



    [Range(-100, 100)]
    public float min_X,min_Y,min_Z;
    [Range(-100, 100)]
    public float max_X, max_Y, max_Z;
    [Space]
    [Range(0, 10)]
    public float speed;
    Vector3 difVector;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        difVector = transform.position - Player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difVecNow = difVector + Player.position;
        if (difVecNow.x < min_X) difVecNow.x = min_X; if (difVecNow.x > max_X) difVecNow.x = max_X;
        if (difVecNow.y < min_Y) difVecNow.y = min_Y; if (difVecNow.y > max_Y) difVecNow.y = max_Y;
        if (difVecNow.z < min_Z) difVecNow.z = min_Z; if (difVecNow.z > max_Z) difVecNow.z = max_Z;
        transform.position = Vector3.Lerp(transform.position,difVecNow, speed * Time.deltaTime);
    }
}
