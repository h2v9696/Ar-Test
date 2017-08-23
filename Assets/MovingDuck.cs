using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDuck : MonoBehaviour
{
//    [SerializeField]
//    public float speed;
//    [SerializeField]
//    public float turnRate;
    public bool isMoving;
    Vector3 targetPos;

    void OnEnable()
    {
//        speed = 50f;
//        turnRate = 20f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        { 
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out hit))
            {
                targetPos = hit.point;
            }
            isMoving = true;
        }
        if (isMoving)
        {
//            PhotonView photonView = PhotonView.Get(this);
//            photonView.RPC("Move", PhotonTargets.All);
            Move();
        }
            
    }

//    [PunRPC]
    void Move()
    {
        var targetRotation = Quaternion.LookRotation(targetPos - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);

//        transform.LookAt (targetPos);
        transform.position = Vector3.MoveTowards (transform.position, targetPos, 50f * Time.deltaTime);
        if (Vector3.Distance (transform.position, targetPos) <= 0.1f)
        {
            isMoving = false;
        }
    }
}
