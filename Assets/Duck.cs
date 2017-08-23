﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class Duck : DuckBehavior
{

    /// <summary>
    /// The speed that this cube should move by when there are axis inputs
    /// </summary>
    public float speed = 5.0f;
    public bool isMoving;
    Vector3 targetPos;
    private void Update()
    {
        // If this is not owned by the current network client then it needs to
        // assign it to the position and rotation specified
        if (!networkObject.IsOwner)
        {
            // Assign the position of this cube to the position sent on the network
            transform.position = networkObject.position;

            // Assign the rotation of this cube to the rotation sent on the network
            transform.rotation = networkObject.rotation;

            // Stop the function here and don't run any more code in this function
            return;
        }

            if (Input.GetMouseButtonDown (0))
            { 
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

                if (Physics.Raycast (ray, out hit))
                {
                    targetPos = hit.point;  
                Debug.Log (targetPos);
                isMoving = true;

                }
            }
            if (isMoving)
            {
                //            PhotonView photonView = PhotonView.Get(this);
                //            photonView.RPC("Move", PhotonTargets.All);
                Move ();
            }
//        // Get the movement based on the axis input values
//        Vector3 translation = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
//
//        // Scale the speed to normalize for processors
//        translation *= speed * Time.deltaTime;
//
//        // Move the object by the given translation
//        transform.position += translation;
//
//        // Just a random rotation on all axis
//        transform.Rotate(new Vector3(speed, speed, speed) * 0.25f);

        // Since we are the owner, tell the network the updated position
        networkObject.position = transform.position;

        // Since we are the owner, tell the network the updated rotation
        networkObject.rotation = transform.rotation;

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually
    }
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
