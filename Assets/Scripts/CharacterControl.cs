using UnityEngine;
using System.Collections;

public class CharacterControl : CombatStats {

    public GameObject cube;

    public Vector3 startMarker;
    public Vector3 endMarker;
    public float speed = 8.0F;
    private CharacterController controller;

    private const float STOP_THRESHOLD = 0.5f;
    private const float GRAVITY = 700f;

    private Vector3 moveDirection = Vector3.zero;

    public GameObject moveTarget;
    public GameObject itemTarget;
    private NavMeshAgent agent;
    private float itemRange;
    private PlayerState playerState = PlayerState.idle;


    //this is your object that you want to have the UI element hovering over
    public GameObject WorldObject;

    //this is the ui element
    public RectTransform rectTransform;

    enum PlayerState
    {
        idle,
        walk,
        combat,
        pickup
    }
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();

        startMarker = transform.position;
        endMarker = transform.position;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        moveTarget.transform.position = transform.position;
        itemRange = 2.3f;
    }

    void Update()
    {
        agent.SetDestination(moveTarget.transform.position);

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2);

                if(hit.transform.tag == "pickup")
                {
                    playerState = PlayerState.pickup;
                    itemTarget = hit.transform.gameObject;
                } else if(hit.transform.tag == "combat")
                {
                    playerState = PlayerState.combat;
                    EnemyTarget = hit.transform.gameObject;
                } else
                {
                    playerState = PlayerState.walk;
                }


                startMarker = transform.position;
                endMarker = hit.point;
                endMarker.y = startMarker.y;

                moveTarget.transform.position = endMarker;
                //UpdateTargetDir();
            }
        }

        if(playerState == PlayerState.pickup && Vector3.Distance(transform.position, itemTarget.transform.position) < itemRange)
        {
            Destroy(itemTarget);
            itemTarget = null;
            playerState = PlayerState.walk;
            //ConvertToUI();
        } else if(playerState == PlayerState.combat)
        {
            Attack();
        } else if(playerState == PlayerState.walk)
        {
            if (agent.velocity.magnitude <= 0.1f)
            {
                playerState = PlayerState.idle;
            }
        }


    }

    void ConvertToUI()
    {

        Vector2 pos = WorldObject.transform.position;  // get the game object position
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        print(viewportPoint);
        rectTransform.anchorMin = viewportPoint;
        rectTransform.anchorMax = viewportPoint;
    }

}
