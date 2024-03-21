using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class G_EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chasing,
        Moving,
        Attacking,
        Dead,
    }

    public ParticleSystem muzzleFlash;
    public AudioClip fireSound;
    public Transform shootPoint;

    //public int maxHealth = 100;
    //public int health = 100;
    public int damage = 10;
    public State state = State.Idle;
    public Transform destination;

    private bool isDead;
    private bool alreadyAttacked = false;
    public GameObject[] players = new GameObject[10];
    public float[] distances = new float[10];
    public float timer;
    public float shootTimer;
    public float speedThreshold = 0.3f;


    private float minDistance;
    private float shootRate = 2.0f;
    private float chaseRange = 150f;
    private float attackRange = 50f;

    private float timeBetweenAttacks = 1.0f;

    private NavMeshAgent nav;
    private AudioSource audioSource;
    private Animator anim;
    private CapsuleCollider col;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        players = GameObject.FindGameObjectsWithTag("Player");
        anim = GetComponent<Animator>();
        destination = CalculateDistances();
        col = GetComponent<CapsuleCollider>();
    }

    //추격 시작 거리 100, 공격 시작 50
    private void Update()
    {
        if(!isDead) {
            switch (state)
            {
                case State.Idle: UpdateIdleState(); break;
                case State.Chasing: UpdateChaseState(); break;
                case State.Attacking: UpdateAttackState(); break;
                case State.Dead: UpdateDeadState(); break;
            }

            if (GetComponentInChildren<cshDamageHP>().hp <= 0)
                state = State.Dead;

            timer += Time.deltaTime;
            //일정 시간마다 거리값 계산
            if (timer >= 2.0f)
            {
                destination = CalculateDistances();
                timer = 0f;
            }

            if(shootTimer < shootRate)
            {
                shootTimer += Time.deltaTime;
            }

            //Compute the speed
            Vector3 speed = transform.position / Time.deltaTime;
            speed.y = 0;
            //local Speed
            Vector3 localSpeed = transform.InverseTransformDirection(speed);
            anim.SetFloat("directionX", Mathf.Clamp(localSpeed.x, -1, 1));
            anim.SetFloat("directionY", Mathf.Clamp(localSpeed.z, -1, 1));
        }
    }

    private void UpdateIdleState()
    {
        //Idle일때는 대기하고있다가, 거리를 만족하면 추격한다
        nav.isStopped = true;
        anim.SetBool("isMoving", false);
        nav.SetDestination(destination.position);
        //destination = CalculateDistances();
        if(destination != null)
        {
            float distance = Vector3.Distance(transform.position, destination.position);
            if (distance <= chaseRange)
            {
                state = State.Chasing;
                nav.isStopped = false;
            }
        }
    }

    private void UpdateChaseState()
    {
        if (destination != null)
        {
            //추격상태가 되면 거리가 가장 가까운 플레이어를 타깃으로 삼는다.
            //destination = CalculateDistances();
            nav.SetDestination(destination.position);
            nav.isStopped = false;
            anim.SetBool("isMoving", true);
            transform.LookAt(destination.position);

            //목적지 플레이어가 유효하면 공격하고, 아니면 다른 추격대상을 삼아야함..
            if (destination)
            {
                //거리가 일정치 이상 가까워지면 공격한다
                float distance = Vector3.Distance(transform.position, destination.position);
                if (distance <= attackRange)
                {
                    state = State.Attacking;
                }
                //너무 멀어지면 추격을 중단한다
                else if (distance >= chaseRange)
                {
                    state = State.Idle;
                }
            }
            else
            {
                destination = CalculateDistances();
            }
            
        }   
    }

    private void UpdateAttackState()
    {
        //플레이어가 유효해야함
        if(destination)
        {
            transform.LookAt(destination.position);
            float distance = Vector3.Distance(transform.position, destination.position);
            if (distance <= attackRange)
            {
                transform.LookAt(destination.position);
                nav.isStopped = true;
                anim.SetBool("isMoving", false);

                AttackPlayer();
            }
            else if (distance > attackRange && distance < chaseRange)
            {
                nav.isStopped = false;
                state = State.Chasing;
            }
            else if (distance > chaseRange)
            {
                state = State.Idle;
            }
        }
    }

    private Transform CalculateDistances()
    { 
        //플레이어들 간의 거리를 계산하는 함수
        int i = 0;
        GameObject temp = null;
        minDistance = 1000000f;
        foreach(GameObject player in players)
        {
            //플레이어가 죽는 등 해서 없어지면, 쫓지 않는다
            if (!player)
            {
                distances[i] = 1000000f;
                players[i] = null;
            }
            else
            {   //거리값을 계산해서 대입한다
                distances[i] = Vector3.Distance(transform.position, player.transform.position);

                if(distances[i] < minDistance)
                {   //거리가 제일 가까운 플레이어를 타겟팅
                    minDistance = distances[i];
                    temp = player;
                }
            }
            i++;
        }

        return temp.transform;
    }

    private void UpdateDeadState()
    {
        nav.isStopped = true;
        anim.SetBool("isMoving", false);
        anim.SetBool("isDead", true);
        col.enabled = false;
        Destroy(gameObject, 5f);
    }

    private void AttackPlayer()
    {
        if(!alreadyAttacked)
        {
            Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(fireSound);
        anim.SetTrigger("doShoot");
        RaycastHit hit; //Ray생성 
        Vector3 dir = destination.position - shootPoint.position;
        Debug.DrawRay(shootPoint.position, dir, Color.green, 3.0f);
        if (Physics.Raycast(shootPoint.position, dir, out hit, attackRange + 50)) //충돌되는 object가 있는지 검사 
        {
            muzzleFlash.Play();
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                G_DamagableScript obj = hit.transform.GetComponent<G_DamagableScript>();
                if (obj != null)
                {
                    Debug.Log("player detected");
                    int n = Random.Range(0, 9);
                    //if (n > 4)
                    obj.TakeDamage(5);
                }

                Debug.Log("player hit");
            }
        }
    }
}
