using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject camera;
    public Transform transform;
    public Vector2 sensivity;
    public Vector2 rotation;
    public RectTransform aim;
    public Quaternion angle;


    void Start()
    {
        camera = Camera.main.gameObject;
        transform = camera.GetComponent<Transform>();
    }

    void Update()
    {
        /*Vector2 input = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        rotation += input * sensivity * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -15f, 15f);
        rotation.x = Mathf.Clamp(rotation.x, -20f, 20f);
        Debug.Log(rotation);
        aim.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
        angle = aim.rotation;
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.01f);*/

        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        rotation += input * sensivity * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -10f, 10f);
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        aim.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
        angle = aim.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.05f);
    }
}
