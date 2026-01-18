using TMPro;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject waypoints;
    [SerializeField] GameObject carUI;
    [SerializeField] TextMeshProUGUI requestMessage;
    [SerializeField] int maxBoxRequest = 3;
    private Transform target;
    private int wavepointIndex = 0;
    private int boxAmountRequest = 1;
    private string boxTagRequest = "BurgerBox";

    enum CarState
    {
        Moving,
        Stopped,
        KeepMoving
    }

    CarState state = CarState.Moving;
    void OnEnable()
    {
        UpgradePoint.CarCanMove += ResetPath;
    }
    void OnDisable()
    {
        UpgradePoint.CarCanMove -= ResetPath; 
    }
    // Start is called before the first frame update
    void Start()
    {
        state = CarState.Stopped;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == CarState.Stopped)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        if(wavepointIndex == waypoints.transform.childCount - 1 && state == CarState.Moving) // Last waypoint before reset
        {
            CreateBuyRequest();
        }
    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= waypoints.transform.childCount - 1)
        {
            ResetPath();
            return;
        }
        wavepointIndex++;
        target = waypoints.transform.GetChild(wavepointIndex);
    }

    private void OnMouseDown()
    {
        #if UNITY_EDITOR 
        Debug.Log("Debug buy food"); 
        #endif
        if (state == CarState.Stopped)
        {
            carUI.SetActive(false);
            state = CarState.KeepMoving;
        }
    }

    void CreateBuyRequest()
    {
        state = CarState.Stopped;
        carUI.SetActive(true);
        //carUI.transform.LookAt(Camera.main.transform);
        carUI.transform.position = transform.position + new Vector3(0f, 4f, 0f);
        boxAmountRequest = Random.Range(1, maxBoxRequest + 1);
        int typeBoxRequest = Random.Range(0, TradeManager.Instance.GetSizeOfBoxTagList());
        boxTagRequest = TradeManager.Instance.GetFoodBoxTag(typeBoxRequest);
        requestMessage.text = boxAmountRequest.ToString() + " " + boxTagRequest;
    }

    public void BuyABox()
    {
        boxAmountRequest--;
        requestMessage.text = boxAmountRequest.ToString() + " " + boxTagRequest;
        if (boxAmountRequest == 0)
        {
            if (state == CarState.Stopped)
            {
                carUI.SetActive(false);
                state = CarState.KeepMoving;
            }
        }
    }

    public bool IsWaitingToBuy()
    {
        return boxAmountRequest > 0;
    }

    public string GetTagRequest()
    {
        return boxTagRequest;
    }

    void ResetPath()
    {
        transform.position = waypoints.transform.position;
        transform.transform.rotation = Quaternion.identity;
        target = waypoints.transform.GetChild(0);
        state = CarState.Moving;
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Random.ColorHSV();
        wavepointIndex = 0;
    }
}
