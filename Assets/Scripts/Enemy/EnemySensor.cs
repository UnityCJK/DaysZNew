using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    //private Enemy enemy;
    [SerializeField]
    public float v_angle;
    [SerializeField]
    public float v_distance;
    [SerializeField]
    LayerMask v_layer;


    private void Update()
    {
        EnemySee();
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle *Mathf.Deg2Rad),0f,Mathf.Cos(_angle*Mathf.Deg2Rad));
    }
    void EnemySee()
    {
        Vector3 _leftBoundary = BoundaryAngle(-v_angle *0.5f);
        Vector3 _rightBoundary = BoundaryAngle(v_angle *0.5f);

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.green);

        Collider[] _target = Physics.OverlapSphere(transform.position, v_distance, v_layer);

        if(_target.Length>0)
        {
            Transform _targetTf = _target[0].transform;

            Vector3 _direction = (_targetTf.position - transform.position).normalized;
            float t_angle = Vector3.Angle(_direction, transform.forward);
            if(t_angle < v_angle *0.5f)
            {
                if(Physics.Raycast(transform.forward, _direction, out RaycastHit hit, v_distance))
                {
                    if(hit.transform.name == "Player")
                    {
                       // enemy.playerSee = true;
                        //enemy.Destination();
                    }
                }
            }

        }
        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name == "Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);
            }
        
        }
    }
}
