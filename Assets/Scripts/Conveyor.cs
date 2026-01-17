using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] Store store;
    [SerializeField] Receiver receiver;
    [SerializeField] float countDownTime = 1f;
    float speed;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 distance = endPoint.transform.position - startPoint.transform.position;
        speed = distance.magnitude / countDownTime;
        obj = store.GetObj();
        obj.transform.position = startPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((obj.transform.position - endPoint.transform.position).magnitude >= speed * Time.deltaTime)
        {
            obj.transform.Translate((endPoint.transform.position - startPoint.transform.position).normalized * speed * Time.deltaTime);
        }
        else
        {
            if (receiver.IsFull())
            {
                Destroy(obj);
            }
            else
            {
                receiver.GetObject(obj);
            }
            obj = store.GetObj();
            obj.transform.position = startPoint.transform.position;
        }
    }
}
