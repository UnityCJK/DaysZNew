using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName; // ���̸�
    public float fireRate;  // ����ӵ�
    public float reloadTime;  //������ �ӵ�
    public float range;
    public int damage;  //�� ������

    public int reloadBulletCount; //��������
    public int curBulletCount; //�����Ѿ˼�
    public int maxBulletCount; // �ִ�������� �Ѿ˼�
    public int carryBulletCount; // ���� ���� �Ѿ˼�

    public Animator gunAnim;

    public ParticleSystem gunFlash;
    public AudioSource gunSound;

    public enum Type
    {
        DBSG,
        M16,
        M18
    }
    public void BulletFill()
    {
        GameObject DBSG = GameObject.Find("DBSG");
        GameObject Rifle = GameObject.Find("M16");
    }

    }
