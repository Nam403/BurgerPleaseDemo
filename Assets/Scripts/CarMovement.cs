using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject waypoints;
    [SerializeField] GameObject carUI;
    private Transform target;
    private int wavepointIndex = 0;

    enum CarState
    {
        Moving,
        Stopped,
        KeepMoving
    }

    CarState state = CarState.Moving;
    // Start is called before the first frame update
    void Start()
    {
        ResetPath();
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
            state = CarState.Stopped;
            carUI.SetActive(true);
            carUI.transform.position = transform.position + new Vector3(0f, 4f, 0f);
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
        Debug.Log("Debug buy food");
        if (state == CarState.Stopped)
        {
            carUI.SetActive(false);
            state = CarState.KeepMoving;
        }
    }

    void ResetPath()
    {
        transform.position = waypoints.transform.position;
        transform.transform.rotation = Quaternion.identity;
        target = waypoints.transform.GetChild(0);
        state = CarState.Moving;
        wavepointIndex = 0;
    }
}
