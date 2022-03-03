using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    
    public MultiJoy multiJoy;
    public GameObject runBtn;
    public SceneControl sceneControl;

    //ü��
    [SerializeField]
    float hp; 
    float curHp;
    float hpSpeed = 0.1f;
    //���׹̳�
    [SerializeField]
    float sp;
    float curSp;
    //���׹̳� ������
    [SerializeField]
    int spSpeed;
    //���׹̳� ȸ����
    [SerializeField]
    float spRechargingTime;
    float curSpRechargingTime;
    //���׹̳� ����
    bool spUsed;
    //�����
    [SerializeField]
    float hungry;
    float curhungry;
    //����� �ӵ�
    [SerializeField]
    float hungryTime;
    float curHungryTime = 3;

    //�񸶸�
    [SerializeField]
    float thirsty;
    float curthirsty;
    //�񸶸� �ӵ�
    [SerializeField]
    float thirstyTime;
    float curThirstyTime;

    //�Ƿ�
    [SerializeField]
    float fatigue;
    float curfatigue;
    //�Ƿ� �ӵ�
    [SerializeField]
    float fatigueTime;
    float curFatigueTime;
    // �����̴�
    [SerializeField]
    Slider[] sliders;
    private const int HP = 0, SP = 1, HUNGRY = 2, THIRSTY = 3, FATIGUE = 4;
    void Start()
    {
        curHp = hp;
        curSp = sp;
        curhungry = hungry;
        curthirsty = thirsty;
        curfatigue = fatigue;
    }

    void Update()
    {
        Hungry();
        Thirsty();
        SliderUpdata();
        SPRecharging();
        SPRecovery();
        Fatigeu();
        HpRecovery();
    }
    void Hungry()
    {
        if (curhungry > 0)
        {
            if (curHungryTime <= hungryTime)
                curHungryTime++;
            else
            {
                curhungry--;
                curHungryTime = 0;
            }
        }
        else
        {
            curHungryTime += Time.deltaTime;
            if(curHungryTime>=5)
            {
                curHungryTime = 0;
                HpPlus(-2);

            }
        }

    }
    void Thirsty()
    {
        if (curthirsty > 0)
        {
            if (curThirstyTime <= thirstyTime)
                curThirstyTime++;
            else
            {
                runBtn.SetActive(true);
                curthirsty--;
                curThirstyTime = 0;
            }
        }
        else
        {
            runBtn.SetActive(false);

        }
    }
    void Fatigeu()
    {
        if (curfatigue > 0)
        {
            if (curFatigueTime <= fatigueTime)
                curFatigueTime++;
            else
            {
                curfatigue--;
                curFatigueTime = 0;
            }
        }
        else
        {
            multiJoy.moveSpeed = 1.5f;
            multiJoy.moveSpeed = 2.5f;
            if(curfatigue>0)
            {
                multiJoy.moveSpeed = 2.0f;
                multiJoy.runSpeed = 3.5f;

            }
        }
    }
    void SliderUpdata()
    {
        sliders[HP].value = curHp / hp;
        sliders[SP].value = curSp / sp;
        sliders[HUNGRY].value = curhungry / hungry;
        sliders[THIRSTY].value = curthirsty / thirsty;
        sliders[FATIGUE].value = curfatigue / fatigue;
    }
    public void HpRecovery()
    {
        if(thirsty >=50f &&hungry >= 50f &&fatigue>=50f)
        {
            curHp += hpSpeed*Time.deltaTime;
        }
    }

    public void SpUsing(float _count)
    {
        spUsed = true;
        curSpRechargingTime = 0;

        if (curSp - _count > 0)
        {
            curSp -= _count;
        }
        else
            curSp = 0;
    }
    private void SPRecharging()
    {
        if (spUsed)
        {
            if (curSpRechargingTime < spRechargingTime)
            {
                curSpRechargingTime++;
            }
            else
                spUsed = false;
        }
    }
    public void SPRecovery()
    {
        if(!spUsed && curSp <sp)
        {
            curSp += spSpeed*Time.deltaTime;
        }
    }
    public float CurSpZero()
    {
        return curSp;
    }
    public void HpPlus(float _count)
    {
        if (curHp + _count < hp)
        {
            curHp += _count;
            PlayerDead(curHp);
        }
        else
        {
            curHp = hp;
        }    
    }
    void PlayerDead(float _count)
    {
        Debug.Log(_count);
        if(_count <= 0)
        {
            sceneControl.DeadPanel();
        }
    }

    public void HungryPlus(int _count)
    {
        if (curhungry + _count < hungry)
        {
            curhungry += _count;
        }
        else
            curhungry = hungry;
    }
    public void ThirstyPlus(int _count)
    {
        if (curthirsty + _count < thirsty)
        {
            curthirsty += _count;
        }
        else
            curthirsty = thirsty;
    }

    public void FatiguePlus(int _count)
    {
        if (curfatigue + _count < fatigue)
        {
            curfatigue += _count;
        }
        else
            curfatigue = fatigue;
    }
}
