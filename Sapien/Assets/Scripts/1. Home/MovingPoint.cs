using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    MovingPercon MP;
    Camera Cam;
    public GameObject Prefab;
    private Ray RayMouse;
    private GameObject Instance;
    Vector3 go;
    bool second;
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MP = GetComponent<MovingPercon>();
    }
    void Update()
    {
        MP.SecondMoving(second, go);
        if (Input.GetButtonDown("Fire1"))
        {
            if (Cam != null)
            { 
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                RayMouse = Cam.ScreenPointToRay(mousePos);

                if (Physics.Raycast(RayMouse.origin, RayMouse.direction, out hit, 40))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        Instance = Instantiate(Prefab);
                        Instance.transform.position = hit.point + hit.normal * 0.01f;
                        second = true;
                        MP.second = false;
                        go = Instance.transform.position;
                        Destroy(Instance, 1.5f);
                        
                    }
                }
            }
            else
            {
                Debug.Log("No camera");
            }
        }
    }
}
