using UnityEngine;

public class Receiver : CarriedObjContainer
{
    [SerializeField] float distanceReceiver = 0.5f;
    [SerializeField] float countDownTime = 0.25f;
    [SerializeField] string objTag = "Burger"; 
    float countDownRec;
    void Start()
    {
        base.Start();
        countDownRec = countDownTime;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 distance = CharacterCarry.Instance.transform.position - transform.position;
        if (distance.magnitude <= distanceReceiver)
        {
            #if UNITY_EDITOR
            Debug.Log("Character is near by");
            #endif
            if (countDownRec <= 0)
            {
                ReceiveObject();
                countDownRec = countDownTime;
            }
            else
            {
                countDownRec -= Time.deltaTime;
            }
        }
        else
        {
            countDownRec = countDownTime;
        }
    }

    void ReceiveObject()
    {
        if (CharacterCarry.Instance.HaveObjWithTag(objTag) == false)
        {
            return;
        }
        GameObject obj = CharacterCarry.Instance.DropCarriedObject(objTag);
        GetObject(obj);
    }
    public string GetObjTag()
    {
        return objTag;
    }
}
