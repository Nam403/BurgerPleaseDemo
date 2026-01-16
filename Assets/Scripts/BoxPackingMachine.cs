using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.Editor;
using UnityEngine;

public class BoxPackingMachine : MonoBehaviour
{
    [SerializeField] GameObject foodPrefab;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] GameObject foodReceiver;
    [SerializeField] GameObject boxDistributor;
    [SerializeField] float countDownPacking = 0.5f;
    float countDownTime;
    // Start is called before the first frame update
    void Start()
    {
        countDownTime = countDownPacking;
    }

    // Update is called once per frame
    void Update()
    {
        if(foodReceiver.GetComponent<Receiver>().GetAmountObj() < foodPrefab.GetComponent<Food>().GetAmountForBox())
        {
            return;
        }
        if (boxDistributor.GetComponent<Distributor>().IsFull())
        {
            return;
        }
        if(countDownTime <= 0)
        {
            SpawnBox();
            countDownTime = countDownPacking;
        }
        else
        {
            countDownTime -= Time.deltaTime;
        }

    }
    void SpawnBox()
    {
        string tag = foodReceiver.GetComponent<Receiver>().GetObjTag();
        int index;
        for (int i = 0; i < foodPrefab.GetComponent<Food>().GetAmountForBox(); i++)
        {
            index = foodReceiver.GetComponent<Receiver>().GetIndexWithTag(tag);
            GameObject food = foodReceiver.GetComponent<Receiver>().DropObject(index);
            Destroy(food);
        }
        Debug.Log("Create box!");
        GameObject box = Instantiate(boxPrefab, transform.position, Quaternion.identity);
        boxDistributor.GetComponent<Distributor>().GetObject(box);
    }
}
