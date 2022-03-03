using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public GameObject curRander;
    List<GameObject> walls = new List<GameObject>();
    
    void Update()
    {
        CamFollow();
        FadeOutWall();
    }
    void CamFollow()
    {
        transform.position = target.position + offset;
    }
    void FadeOutWall()
    {
        float Distance = Vector3.Distance(transform.position, target.transform.position);
        Vector3 Direction = (target.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Direction, out hit, Distance))
        {
            Debug.DrawRay(transform.position, Direction * Distance, Color.red);
            curRander = hit.transform.gameObject;
            walls = walls.Distinct().ToList();
            if (curRander.tag == "Player")
            {
                if (walls.Count >= 1)
                {
                    GameObject oldWall = walls[0];
                    oldWall.GetComponent<MeshRenderer>().enabled = true;
                    walls.Clear();
                    return;
                }
            }
            if (curRander.tag == "Wall")
            {

                walls.Add(curRander);
                curRander.GetComponent<MeshRenderer>().enabled = false;
                walls = walls.Distinct().ToList();
            }

                //foreach(GameObject curWall in walls)
                //{
                //    GameObject oldWall = walls[0];
                //    if(curWall != oldWall)
                //    {
                //        oldWall.GetComponent<MeshRenderer>().enabled = true;

                //        walls.Clear();
                //        return;
                //    }

                //}

             

        }
    }
}

