using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{

    Rigidbody metalRb;
    GameObject[] magnets;
    public float magneticForceConstant = 1f;

    private void Start()
    {
        metalRb = GetComponent<Rigidbody>();
        GetMagnets();
    }

    void GetMagnets()
    {
        magnets = GameObject.FindGameObjectsWithTag(Utility.DraggableTag);
    }

    private void FixedUpdate()
    {

        foreach (GameObject magnet in magnets)
        {
            float distance = Vector3.Distance(transform.position, magnet.transform.position);
            Vector3 direction = magnet.transform.position - transform.position;
            Vector2 magneticForce = CalculateMagneticForce(distance, direction);
            metalRb.AddForce(magneticForce.x, 0, magneticForce.y, ForceMode.Force);
        }

    }

    Vector2 CalculateMagneticForce(float distance, Vector3 direction)
    {
        return new Vector2(MagneticForceCalculateHelper(distance, direction.x), MagneticForceCalculateHelper(distance, direction.z));
    }

    float MagneticForceCalculateHelper(float distance, float direction)
    {
        return direction * magneticForceConstant / Mathf.Pow(distance, 2);
    }


}
