using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    Animator enemyAnim;
    public GameObject enemy;
    public Transform target;
    public LayerMask playerTarget;
    public float moveSpeed;
    NavMeshAgent nav;
    public BoxCollider enemycollider;
    public GunController gunController;
    public WeaponManager weaponManager;

    public ParticleSystem enemyHitParticle;
    public MeleeAtk meleeAtk;
    //좀비 스텟
    public int hp = 100;
    public int damage = 20;
    public float atkDelay;
    public float atkCoolTime = 2f;
    public float lastAtk;
    public int enemyView;
    public float curTime;
    public float deadTime;

    public float soundTime;

    public float walkSpeed = 1f;
    public float runSpeed = 1.5f;

    public bool dead = false;

    public Transform EnemyAtkPos;
    public GameObject objCol;
    public AudioSource deadSound;
    public AudioSource enemySound;
    void Awake()
    {
        weaponManager = GameObject.Find("Player").GetComponent<WeaponManager>();
        meleeAtk = GameObject.Find("Player").GetComponent<MeleeAtk>();
        gunController = GameObject.Find("Player").GetComponent<GunController>();
        enemyView = Random.Range(10, 15);
        target = GameObject.Find("Player").GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        objCol = EnemyAtkPos.GetChild(0).gameObject;
        enemycollider = objCol.GetComponent<BoxCollider>();
        enemycollider.enabled = false;

    }
    private void OnEnable()
    {
        enemy.gameObject.tag = "Enemy";
        hp = 100;
        nav.isStopped = false;
        dead = false;
        enemySound.Play();
        StartCoroutine(UpdateEnemy());
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime>=120)
        {
            curTime = 0f;
            enemyView *= 2;
            hp += 50;
        }
    }
    void NavZombie(Vector3 _target)
    {
        nav.SetDestination(target.position);
    }
    IEnumerator UpdateEnemy()
    {
            while (!dead)
            {
                StartCoroutine(enemyTarget());
                StartCoroutine(moveEnemy());
                yield return new WaitForSeconds(1f);
            }
    }
    IEnumerator enemyTarget()
    {
        Vector3 dir = target.position - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir),5 * Time.deltaTime);
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator moveEnemy()
    {
            if ((target.position - transform.position).magnitude <= enemyView)
            {
                nav.speed = walkSpeed;
                NavZombie(target.position);

                enemyAnim.SetBool("Run", true);

            if ((target.position - transform.position).magnitude <= 1f)
            {
                nav.isStopped = true;
                nav.velocity = Vector3.zero;
                gameObject.transform.LookAt(target);
                StartCoroutine(Attack());
            }
            if ((target.position - transform.position).magnitude > 1f)
                {
                     nav.isStopped = false;
                     enemyAnim.SetBool("Run", true);
                }
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
            }
            if ((target.position - transform.position).magnitude > enemyView)
            {
            nav.speed = walkSpeed;
            enemyAnim.SetBool("Run", false);
                nav.isStopped = true;
            }
        yield return new WaitForSeconds(1f);
    }
    IEnumerator Attack()
    {
        nav.isStopped = true;
        if(!dead)
        {
            enemyAnim.SetTrigger("Attack");
            yield return new WaitForSeconds(1.5f);
            nav.isStopped = false;
        }
    }

    void EnemyAtking()
    {
        enemycollider.enabled = true;
    }
    void EnemyAtkEnd()
    {
        enemycollider.enabled = false;
    }
    void StopEnemy()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
    }
    void StartEnemy()
    {
        nav.isStopped = false;
    }
    IEnumerator Damage()
    {
        nav.velocity = Vector3.zero;
        if (!dead)
        {
            deadSound.Play();
            enemyHitParticle.Play();
            if (weaponManager.curWeaponType == "Gun")
            {
                hp -= gunController.curGun.damage;

            }
            else if (weaponManager.curWeaponType == "Melee")
            {
                hp -= WeaponManager.curWeapon.GetComponent<MeleeWeapon>().AttackPower;

            }
            enemyAnim.SetTrigger("Damage");
            if (hp <= 0)
            {
                dead = true;
                EnemyDead();
            }
            yield return new WaitForSeconds(1.5f);
            nav.velocity = Vector3.zero;
        }
    }

    void EnemyDead()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
        enemy.gameObject.tag = "Dead";
        enemyAnim.SetTrigger("Dead");
        Invoke("DeadTime", 2);
    }
    public void HitGrenade()
    {
        hp -= Random.Range(90, 120);
        if (hp <= 0)
        {
            dead = true;
            EnemyDead();
        }
    }
    void DeadTime()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            StartCoroutine(Damage());
        }
    }
    //void Update()
    //{
    //    if(enableAct)
    //    {
    //        enemyTarget();
    //        moveEnemy();
    //    }
    //}
    //void StopEnemy()
    //{
    //    nav.isStopped = true;
    //    nav.velocity = Vector3.zero;
    //    Debug.Log("스탑");
    //}
    //void GoEnemy()
    //{
    //    nav.isStopped = false;
    //    NavZombie(target.position);
    //    Debug.Log("고고");
    //}
}
