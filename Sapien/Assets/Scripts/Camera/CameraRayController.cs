using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayController : MonoBehaviour
{
  
    Camera Cam;
    private Ray RayMouse;
    
    private void Start()
    {
        Cam = GetComponent<Camera>();
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
                    
                }
            }
            else
            {
                Debug.Log("No camera");
            }
        }
    }
   
}
