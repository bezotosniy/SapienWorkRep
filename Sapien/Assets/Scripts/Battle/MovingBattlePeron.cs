using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

    public class MovingBattlePeron : MonoBehaviour
    {
        public Inventory inv;
        private NavMeshAgent agent;
        public Transform point;
        public Animator Anim;
        [Space]
        public GameObject particleShag;
        public GameObject StartParticle, background, Uron, DestroyBullet;
        public Transform instantiateParticle;
        [Range(0.1f, 10)]
        public float speedBulllet;
        public Transform Vrag;
    
        
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            StartCoroutine(AgentWait());
        }
        public IEnumerator ClickOnEnemy(Vector3 enemyPoint)
        {

            GameObject bk = Instantiate(background, transform);
            Anim.SetTrigger("StartFight");

            yield return new WaitForSeconds(1);
            GameObject bullet = Instantiate(Uron, instantiateParticle);
         

            StartCoroutine(MoveBullet(bullet, enemyPoint));
            yield return new WaitForSeconds(2);
            Destroy(bk);
        }

        IEnumerator MoveBullet(GameObject bullet, Vector3 point)
        {
            while (true)
            {
            Vector3 quat = Vector3.RotateTowards( bullet.transform.forward, point - bullet.transform.position, 120 *Time.deltaTime,0.0f);
            bullet.transform.rotation = Quaternion.LookRotation(quat);
                bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, point,speedBulllet * Time.deltaTime);
                if (Vector3.Distance(bullet.transform.position, point) < 0.01f)
                {
                    inv.EnemyDie();
                 
                    Instantiate(DestroyBullet,Vrag);
                    Destroy(bullet);
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator AgentWait()
        {
            yield return new WaitForSeconds(2);
            agent.SetDestination(point.position);
            StartCoroutine(ParticleShag(particleShag));
        }
        IEnumerator ParticleShag(GameObject particle)
        {
            while (agent.enabled)
            {
                Instantiate(particle, agent.gameObject.transform);
                yield return new WaitForSeconds(Random.Range(1, 2));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, point.position) < .5f)
            {
                agent.enabled = false;
                Anim.SetTrigger("stop");

                Vector3 quat = Vector3.RotateTowards(transform.forward, new Vector3(Vrag.position.x,transform.position.y,Vrag.position.z)-transform.position,100 * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(quat);
            }
        }
    }
