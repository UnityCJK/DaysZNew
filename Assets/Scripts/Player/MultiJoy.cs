using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MultiJoy : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler, IDragHandler
{
    public Transform player;
    Vector3 move;
    public float moveSpeed=2;
    public float runSpeed=3.5f;
    public RectTransform pad;
    public RectTransform stick;
    float spUsingTime;

    public bool walking;
    public bool runing = false;
    public bool isDown = false;
    public bool isStop = false;
    private StatusController statusController;

    //·Ï¿Â
    public float Range;
    public GameObject Target;
    private bool Lockon = false;


    private void Start()
    {
        statusController = FindObjectOfType<StatusController>();
    }
    private void Update()
    {
        UpdateTarget();
    }
    public void OnDrag(PointerEventData eventData)
    {
        stick.position = eventData.position;
        stick.localPosition =
            Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);
            spUsingTime += Time.deltaTime;
        move = new Vector3(stick.localPosition.x, 0, stick.localPosition.y).normalized;
        if(!walking)
        {
            if(runing)
            {
                player.GetComponent<Animator>().SetBool("Run", true);
            }
            walking = true;
            player.GetComponent<Animator>().SetBool("Walk", true);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(isStop == false)
        {
            pad.position = eventData.position;
            pad.gameObject.SetActive(true);
            StartCoroutine("Movement");
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        stick.localPosition = Vector2.zero;
        pad.gameObject.SetActive(false);
        StopCoroutine("Movement");
        move = Vector3.zero;
        if(runing)
        {
            player.GetComponent<Animator>().SetBool("Run", false);
        }
        walking = false;
        player.GetComponent<Animator>().SetBool("Walk", false);
    }
    IEnumerator Movement()
    {
        if (isStop == false)
        {
            while (true)
            {
                player.Translate(move * moveSpeed * Time.deltaTime, Space.World);
                if (Lockon == false)
                {
                    if (move != Vector3.zero)
                        player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(move), 5 * Time.deltaTime);
                }
                if (runing)
                {
                    spUsingTime += Time.deltaTime;
                    if (spUsingTime > 1)
                    {
                        statusController.SpUsing(2);
                        spUsingTime = 0;
                        if (statusController.CurSpZero() <= 0)
                        {
                            moveSpeed = 2f;
                            runing = false;
                            walking = true;
                            player.GetComponent<Animator>().SetBool("Walk", true);
                            player.GetComponent<Animator>().SetBool("Run", false);
                        }
                    }
                }
                yield return null;
            }
        }
    }
    void UpdateTarget()
    {
        if (Lockon)
        {
            if (Target == null)
            {
                GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy");
                float shortDistance = Mathf.Infinity;
                GameObject nearEnemy = null;
                foreach (GameObject Enemy in Enemys)
                {
                    float distanceEnemy = Vector3.Distance(player.transform.position, Enemy.transform.position);
                    if (distanceEnemy <= shortDistance)
                    {
                        shortDistance = distanceEnemy;
                        //Debug.Log("¼îÃ÷" + distanceEnemy);
                        nearEnemy = Enemy;
                        player.transform.LookAt(Enemy.transform);
                        if(shortDistance > Range)
                        {
                            Target = null;
                        }

                    }
                }
            }
        }
    }
    public void Runing()
    {
        if (runing == false)
        {
            Debug.Log("´Þ·Á");
            runing = true;
            moveSpeed = runSpeed;
            return;
        }
        if (runing)
        {
            Debug.Log("°É¾î");
            runing = false;
            moveSpeed = 2f;
            return;
        }
        if (runing == true)
        {
            walking = false;
            player.GetComponent<Animator>().SetBool("Walk", false);
            player.GetComponent<Animator>().SetBool("Run", true);
            moveSpeed = runSpeed;
            runing = false;
            return;
        }
        else if (runing == false)
        {
            walking = true;
            moveSpeed = 2f;
            player.GetComponent<Animator>().SetBool("Run", false);
            player.GetComponent<Animator>().SetBool("Walk", true);
            runing = true;
            return;
        }
    }
    public void LockonOnOff()
    {
        if (Lockon == false)
        {
            Lockon = true;
            return;
        }
        if (Lockon)
        {
            Lockon = false;
            return;
        }
    }
    //public void Dash()
    //{
    //    player.rotation = Quaternion.LookRotation(move);
    //    //player.GetComponent<Animator>().Play("Dodge");
    //    player.GetComponent<Rigidbody>().AddForce(move * dashSpeed, ForceMode.Impulse);
    //}
}
