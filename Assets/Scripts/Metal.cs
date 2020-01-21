using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{

    Rigidbody metalRb;

    void Start()
    {
        metalRb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {

        foreach (GameObject magnet in GameController.Instance.magnets)
        {
            float distance = Vector3.Distance(transform.position, magnet.transform.position);
            Vector3 direction = magnet.transform.position - transform.position;
            Vector2 magneticForce = Utility.Instance.CalculateMagneticForce(distance, direction);
            metalRb.AddForce(magneticForce.x, 0, magneticForce.y, ForceMode.Force);
        }

    }

}
