using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearance : MonoBehaviour
{
    public SkinnedMeshRenderer eye, cloth;
    public MeshFilter hair;
    [Space]
    public Mesh[] eyes_boy,eyes_girl;
    [Space]
    public Mesh[] cloth_Boy,cloth_Girl; 
    public Mesh[] hair_Boy,hair_Girl; 
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("gender") == "male")
        {
            cloth.sharedMesh = cloth_Boy[1];
            eye.sharedMesh = eyes_boy[0];
            hair.sharedMesh = hair_Boy[0];
        }
        else
        {
            cloth.sharedMesh = cloth_Girl[1];
            eye.sharedMesh = eyes_girl[0];
            hair.sharedMesh = hair_Girl[0];
        }
     

    }

    public void WizardMod()
    {
        if (PlayerPrefs.GetString("gender") == "male")
            cloth.sharedMesh = cloth_Boy[1];
        else
            cloth.sharedMesh = cloth_Girl[1];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
