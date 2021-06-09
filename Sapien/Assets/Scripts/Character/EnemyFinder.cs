using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyFinder : MonoBehaviour
{
    [SerializeField]
    private appearance Cloth;
    public ParticleSystem boooooom;
    private Camera Cam;
    private Ray RayMouse;
    GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++){
            enemies[i].SetActive(false);
        }
        Cam = GetComponent<Camera>();
    }
    public void WizardMod()
    {
        
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true);
          
        }
        Instantiate(boooooom.gameObject, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        Cloth.WizardMod();
    }
 
    // Update is called once per frame
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
                {if(hit.collider.tag == "Enemy")
                    StartCoroutine(LoadYourAsyncScene());
                }
            }
        }
    }            // Используем сопрограмму для загрузки сцены в фоновом режиме

    IEnumerator LoadYourAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle 1");

        // Подождите, пока асинхронная сцена полностью загрузится
        while (!asyncLoad.isDone)
        {
            yield return null;
        }



    }
}
