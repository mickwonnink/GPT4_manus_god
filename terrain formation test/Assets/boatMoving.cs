using UnityEngine;
using System.Collections;

public class boatMoving : MonoBehaviour {

    GameObject myBoat;

	// Use this for initialization
	void Start () {
        myBoat = GameObject.Find("fishing_boat");
        goToPosition = myBoat.transform.position;
	}

    Vector3 goToPosition;
    Vector3 startPos = new Vector3();
    float startTime = 0;
    float travelSpeed = 3;

    float distanceMark = 0;

    bool isRotating = false;
    bool hasGoodRotation = false;


    public void OnCollisionEnter(Collision col)
    {
        goToPosition = myBoat.transform.position;
        Debug.Log("DEAD");
    }

    public void PointBoatPosition(Ray hitpos)
    {
        RaycastHit hit;
        if (Physics.Raycast(hitpos, out hit))
        {
            if (hit.collider.gameObject.tag == "clickablePlane")
            {
                goToPosition = new Vector3(hit.point.x, 0, hit.point.z);
                myBoat.transform.LookAt(goToPosition);

                distanceMark = Vector3.Distance(myBoat.transform.position, goToPosition);
                hasGoodRotation = false;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "clickablePlane")
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log(hit.point);
                    goToPosition = new Vector3(hit.point.x, 0, hit.point.z);
                    // myBoat.transform.LookAt(goToPosition);

                    distanceMark = Vector3.Distance(myBoat.transform.position, goToPosition);
                    hasGoodRotation = false;
                }
            }
        }

        if (goToPosition != myBoat.transform.position)
        {

                if (startPos == new Vector3())
                {
                    startPos = myBoat.transform.position;
                    startTime = Time.time;
                }
                //myBoat.transform.position = Vector3.Lerp(startPos, goToPosition, );
                myBoat.transform.position = Vector3.MoveTowards(myBoat.transform.position, goToPosition, Time.deltaTime * travelSpeed);
            

        }
        else
        {
            startTime = 0;
            startPos = new Vector3();
        }

	}
}
