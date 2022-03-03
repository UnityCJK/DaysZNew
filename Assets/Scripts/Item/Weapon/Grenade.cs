using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject meshObj;
    public GameObject effect;
    public GameObject boomSound;

    public Rigidbody rigid;
    public WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GameObject.Find("Player").GetComponent<WeaponManager>();
        gameObject.GetComponent<Grenade>().enabled = false;

    }

    private void Start()
    {
        StartCoroutine(Expltosion());
        weaponManager.attackBtn.SetActive(false);
    }
    IEnumerator Expltosion()
    {

            yield return new WaitForSeconds(3f);
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            effect.SetActive(true);
            boomSound.SetActive(true);
            RaycastHit[] hit = Physics.SphereCastAll(transform.position, 3f, Vector3.up, 0f, LayerMask.GetMask("Enemy"));
        foreach (RaycastHit hitObj in hit)
        {
            if (hitObj.transform.GetComponent<ZombieAi>())
            {
                Debug.Log(hitObj.transform.gameObject.name);
                hitObj.transform.GetComponent<ZombieAi>().HitGrenade();
            }
            if(hitObj.transform.GetComponent<ZombieAiBoss>())
            {
                hitObj.transform.GetComponent<ZombieAiBoss>().HitGrenade();

            }
        }
        meshObj.SetActive(false);
            Destroy(gameObject, 3);
    }


}

