using UnityEngine;
using System.Collections;

public class CharacterControl : CombatStats {
    

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
    public PlayerState playerState = PlayerState.idle;
    private Transform playerMesh;
    private float idleTimeMin = 0.3f;
    private float lastidleTime;
    public LayerMask ignoreWhenNavigating;
    private GameObject animationholder;

    //this is your object that you want to have the UI element hovering over
    public GameObject WorldObject;

    //this is the ui element
    public RectTransform rectTransform;
    Vector3 extension = new Vector3();
    bool callAttack = true;
    float attackSpeed = 0.5f;
    float lastTimeAttackedAnim;
    [System.Serializable]
    public enum PlayerState
    {
        idle,
        walk,
        combat,
        inCombat,
        pickup
    }
    // Use this for initialization
    public override void Start () {
        base.Start();
        range = 4;
        controller = GetComponent<CharacterController>();
        animationholder = GameObject.Find("AnimationHolder");

        startMarker = transform.position;
        endMarker = transform.position;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        moveTarget.transform.position = transform.position;
        itemRange = 2.3f;
        playerMesh = transform.FindChild("Mesh");

        lastidleTime = Time.time;
    }

    void Update()
    {
        agent.SetDestination(moveTarget.transform.position);
        if(playerState != PlayerState.idle)
        {
            if (playerState == PlayerState.combat)
            {
                Vector3 relativePos = moveTarget.transform.position - playerMesh.transform.position;
                relativePos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                playerMesh.rotation = rotation;
            }
            else
            {
                Vector3 moveTargetTempPos = new Vector3(moveTarget.transform.position.x + extension.x, moveTarget.transform.position.y, moveTarget.transform.position.z + extension.z);
                Vector3 relativePos = moveTargetTempPos - playerMesh.transform.position;
                relativePos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                playerMesh.rotation = rotation;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, ignoreWhenNavigating))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2);
                lastidleTime = Time.time;

                if (hit.transform.tag == "Pickup")
                {
                    playerState = PlayerState.pickup;
                    itemTarget = hit.transform.gameObject;
                } else if(hit.transform.tag == "Combat")
                {
                    playerState = PlayerState.combat;
                    enemyTarget = hit.transform.parent.gameObject;
                } else
                {
                    playerState = PlayerState.walk;
                }
                
                HandleAnimation();
                startMarker = transform.position;
                endMarker = hit.point;
                endMarker.y = startMarker.y;

                moveTarget.transform.position = endMarker;
                extension = (moveTarget.transform.position - playerMesh.transform.position).normalized * 5;
                //UpdateTargetDir();
            }
        }
        if(playerState == PlayerState.pickup && Vector3.Distance(transform.position, itemTarget.transform.position) < itemRange)
        {
            Destroy(itemTarget);
            itemTarget = null;
            playerState = PlayerState.walk;
            //ConvertToUI();
        } else if(playerState == PlayerState.combat || playerState == PlayerState.inCombat)
        {
            if (enemyTarget)
            {
                if (enemyTarget.GetComponent<EnemyAttack>().currentHealth > 0)
                {
                    if(Attack())
                    {
                        playerState = PlayerState.inCombat;
                        //HandleAnimation();
                    }
                }
                else
                {
                    playerState = PlayerState.walk;
                }
            } else
            {
                playerState = PlayerState.walk;
            }
        } else if(playerState == PlayerState.walk)
        {
            if (agent.velocity.magnitude <= 0.1f)
            {
                if(Time.time - lastidleTime > idleTimeMin)
                {
                    playerState = PlayerState.idle;

                    //HandleAnimation();
                    lastidleTime = Time.time;
                }
            }
        }
        //print(playerState);
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    //playerState = PlayerState.idle;
                }
            }
        }
        
        HandleAnimation();

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

    void HandleAnimation()
    {
        if (playerState == PlayerState.walk || playerState == PlayerState.combat || playerState == PlayerState.pickup)
        {
            CallPlayerAnimation("Run");
        }
        else if (playerState == PlayerState.inCombat)
        {
            CallPlayerAnimation("Atack");
        }
        else if (playerState == PlayerState.idle)
        {
            CallPlayerAnimation("Idle");
        }
    }


    void CallPlayerAnimation(string animationName)
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Atack", false);
        anim.SetBool("Die", false);
        anim.SetBool("Run", false);
        if(animationName != "Clear")
        {
            anim.SetBool(animationName, true);

        }
    }

}
