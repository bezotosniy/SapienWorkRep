using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public int HPbat = 90;
    public Text enemyText;
    public Slider hpSlider;
    Inventory inv;
    public Animator EnemyAnim;
    public Transform PointShoot;
    public Sprite attack, sleep;
    public Image MouseBar;
    Vector3 StartPosEnemy;
    [Range(0.001f, 1)]
    public float speedMouse;
    // Start is called before the first frame update
    void Start()
    {
       
        MouseBar.sprite = sleep; //change agressive
        MouseBar.gameObject.SetActive(false);
        inv = FindObjectOfType<Inventory>();
        StartPosEnemy = transform.position;
        hpSlider.maxValue = HPbat;
        hpSlider.value = HPbat;
        enemyText.text = hpSlider.value.ToString() + "/" + hpSlider.maxValue.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        HPbat = (int)hpSlider.value;
        enemyText.text = hpSlider.value.ToString() + "/" + hpSlider.maxValue.ToString();

    }
    public IEnumerator EnemyGiveUron(int damage)
    {   if (MouseBar.sprite = attack)
        {
            yield return new WaitForSeconds(2f);
            EnemyAnim.SetBool("Go", true);
            while (true)
            {
                transform.position = Vector3.MoveTowards(transform.position, PointShoot.position, speedMouse);
                Vector3 quat = Vector3.RotateTowards(transform.forward, PointShoot.position - transform.position, 20 * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(quat);
                yield return new WaitForFixedUpdate();
                if (Vector3.Distance(transform.position, PointShoot.position) < 0.01f)
                {
                    EnemyAnim.SetBool("Go", false);
                    EnemyAnim.SetTrigger("Attack");
                    inv.HP_Person_Controller(damage);
                    inv.animPerson.SetTrigger("takeDamage");
                    StartCoroutine(BackEnemyToStartPos());
                    yield break;
                }
            }
        }
    }
    IEnumerator BackEnemyToStartPos()
    {
        yield return new WaitForSeconds(2f);
        EnemyAnim.SetBool("Go", true);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPosEnemy, speedMouse);
            Vector3 quat = Vector3.RotateTowards(transform.forward, StartPosEnemy - transform.position, 10 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(quat);
            yield return new WaitForFixedUpdate();
            if (Vector3.Distance(transform.position, StartPosEnemy) < 0.1f)
            {
                EnemyAnim.SetBool("Go", false);
                transform.rotation = Quaternion.LookRotation(PointShoot.position - transform.position);
               inv.ClickOnGem.interactable = true;
            }
        }
    }
}
