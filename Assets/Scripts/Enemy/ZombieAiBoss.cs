using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAiBoss : MonoBehaviour
{
    public  Animator enemyAnim;
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
    //Á»ºñ ½ºÅÝ
    public int hp = 250;
    public float atkCoolTime = 2f;
    public float lastAtk;
    public int enemyView;
    public float curTime;
    public float deadTime;
    public float soundTime;
    public bool dead = false;

    public Transform EnemyAtkPos;
    public GameObject objCol;
    public AudioSource bossSound;
    public AudioSource bossDeadSound;
    void Awake()
    {
        weaponManager = GameObject.Find("Player").GetComponent<WeaponManager>();
        meleeAtk = GameObject.Find("Player").GetComponent<MeleeAtk>();
        gunController = GameObject.Find("Player").GetComponent<GunController>();
        enemyView = Random.Range(12, 17);
        target = GameObject.Find("Player").GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        objCol = EnemyAtkPos.GetChild(0).gameObject;
        enemycollider = objCol.GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        enemy.gameObject.tag = "Enemy";
        hp = 450;
        nav.isStopped = false;
        dead = false;
        bossSound.Play();
        StartCoroutine(UpdateEnemy());

    }
    void Update()
    {
        soundTime += Time.deltaTime;
        if (soundTime >= 5f)
        {
            soundTime = 0f;
            bossSound.Play();
        }
        curTime += Time.deltaTime;
        if (curTime >= 100)
        {
            curTime = 0f;
            enemyView += 10;
            hp += 100;
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
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator moveEnemy()
    {
        if ((target.position - transform.position).magnitude <= enemyView)
        {
            
            enemyAnim.SetBool("Running", true);
            NavZombie(target.position);
            if ((target.position - transform.position).magnitude <= 0.95f)
            {
                gameObject.transform.LookAt(target);
                nav.velocity = Vector3.zero;
                StartCoroutine(Attack());
                yield return new WaitForSeconds(2.8f);
            }
            if ((target.position - transform.position).magnitude > 0.95f)
            {
                enemyAnim.SetBool("Running", true);
            }
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }
        else if ((target.position - transform.position).magnitude > enemyView)
        {
            enemyAnim.SetBool("Running", false);
        }
        yield return new WaitForSeconds(1f);

    }
    IEnumerator Attack()
    {
        if (!dead)
        {
            enemyAnim.SetTrigger("Attack");

            yield break;
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
        if (!dead)
        {
            bossDeadSound.Play();

            enemyHitParticle.Play();
            if (weaponManager.curWeaponType == "Gun")
            {
                hp -= gunController.curGun.damage;
                nav.velocity = Vector3.zero;
            }

            if (weaponManager.curWeaponType == "Melee")
            {
                hp -= WeaponManager.curWeapon.GetComponent<MeleeWeapon>().AttackPower;
                nav.velocity = Vector3.zero;
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
        enemy.gameObject.tag = "Dead";
        nav.velocity = Vector3.zero;
        enemyAnim.SetTrigger("Dead");
        Invoke("DeadTime", 3);
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
            nav.velocity = Vector3.zero;
            StartCoroutine(Damage());
        }
    }
}
