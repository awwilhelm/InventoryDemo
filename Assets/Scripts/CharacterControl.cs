using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

    public GameObject cube;

    public Vector3 startMarker;
    public Vector3 endMarker;
    public float speed = 20.0F;
    public float otherSpeed = 1f;
    private float startTime;
    private float journeyLength;
    private CharacterController controller;

    private const float STOP_THRESHOLD = 0.5f;

    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();

        startMarker = transform.position;
        endMarker = transform.position;

        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 noHeightEndMarker;
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2);

                startMarker = transform.position;
                endMarker = hit.point;
                endMarker.y = startMarker.y;
                //startMarker = transform.position;
                //endMarker = hit.point;
                //endMarker.y = startMarker.y;
                //startTime = Time.time;
                //journeyLength = Vector3.Distance(startMarker, endMarker);
                //transform.position = hit.point;
                UpdateTargetDir();
                Instantiate(cube, endMarker, Quaternion.identity);
            }

        }

        UpdateTargetDir();

        if (Mathf.Abs(transform.position.x - endMarker.x) > STOP_THRESHOLD || Mathf.Abs(transform.position.z - endMarker.z) > STOP_THRESHOLD)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }

        //float distCovered = (Time.time - startTime) * speed;
        //float fracJourney = journeyLength != 0 ? distCovered / journeyLength : 0;
        //transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
    }

    void UpdateTargetDir()
    {
        Vector3 noHeightEndMarker = endMarker - transform.position;
        noHeightEndMarker.y = 0;
        moveDirection = Vector3.Normalize(noHeightEndMarker);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= otherSpeed;

    }
}
