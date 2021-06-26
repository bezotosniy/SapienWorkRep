using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    public GameObject picture;
    public GameObject camera;

    void Update()
    {
        GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnClick()
    {
        picture.SetActive(true);
        picture.GetComponent<RectTransform>().position = this.gameObject.GetComponent<RectTransform>().position;
        GameObject.Find("PhoneButton").GetComponent<Animator>().Play("Picture");
        camera.GetComponent<CameraManager>().enabled = false;
        this.enabled = false;
    }
}
