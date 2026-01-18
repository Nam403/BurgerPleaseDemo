using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 720f; // degrees per second
    [SerializeField] float planeY = 0f;
    private Animator animator;
    void Start() 
    { 
        animator = GetComponent<Animator>();
    }

    void Update() 
    {
        if (Input.GetMouseButton(0)) 
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Plane plane = new Plane(Vector3.up, new Vector3(0, planeY, 0));
            
            Vector3 worldPos = transform.position;
            /*RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                worldPos = hit.point; 
            }*/
            if (plane.Raycast(ray, out float enter)) 
            { 
                worldPos = ray.GetPoint(enter);
                #if UNITY_EDITOR
                Debug.Log("World pos on plane: " + worldPos);
                #endif
            }
            worldPos.y = transform.position.y;
            Vector3 forward = worldPos - transform.position;
            animator.SetBool("isRunning", true);
            forward.y = 0; 
            forward.Normalize(); 
            transform.position += forward * moveSpeed * Time.deltaTime;
            if (forward != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        #if UNITY_EDITOR
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle")) 
        { 
            Debug.Log("Animator's state: Idle"); 
        } 
        else if (stateInfo.IsName("Run")) 
        { 
            Debug.Log("Animator's state: Run");
        }
        Debug.Log("Check isRunning: " + animator.GetBool("isRunning"));
        #endif
    }
}
