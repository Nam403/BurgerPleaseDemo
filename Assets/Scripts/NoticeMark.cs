using UnityEngine;

public class NoticeMark : MonoBehaviour
{
    [SerializeField] float distanceMoveY = 1f;
    [SerializeField] float speed = 1f;
    private Vector3 rootPosition;
    private bool up;
    // Start is called before the first frame update
    void Start()
    {
        rootPosition = transform.position;
        up = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(up == true) 
        {
            if(transform.position.y < rootPosition.y + distanceMoveY)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            else
            {
                up = false;
            }
        }
        else
        {
            if(transform.position.y > rootPosition.y - distanceMoveY)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            else
            {
                up = true;
            }
        }
    }
}
