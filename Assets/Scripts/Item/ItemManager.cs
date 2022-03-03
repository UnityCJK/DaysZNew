using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> itemPoints = new List<GameObject>();
    public GameObject[] Items;
    public float ItemRespawnTime = 2f;
    public int maxItem = 50;
    public bool isEnd = false;

    private void Start()
    {
        if (itemPoints.Count > 0)
        {
            StartCoroutine(RespawnItem());
        }
    }

    IEnumerator RespawnItem()
    {
        while (!isEnd)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items.Length < maxItem)
                {
                    yield return new WaitForSeconds(ItemRespawnTime);
                    int idx = Random.Range(1, itemPoints.Count);
                    GameObject itemPos = itemPoints[idx];
                    Debug.Log(itemPoints[idx]);
                    GameObject curItem = Items[Random.Range(1, Items.Length)];
                    Debug.Log("»ý¼º");
                    Instantiate(curItem, itemPos.transform.position, itemPos.transform.rotation);
                    itemPoints = itemPoints.Distinct().ToList();
                }
            }
            yield return null;
        }
    }


}


