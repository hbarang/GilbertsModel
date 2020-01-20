using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    public Polarization polarization = Polarization.Positive;

    Rigidbody magnetRb;

    void Start()
    {
        magnetRb = GetComponent<Rigidbody>();
        ChangeColor();
    }

    void FixedUpdate()
    {

        foreach (GameObject metal in GameController.Instance.metals)
        {
            float distance = Vector3.Distance(transform.position, metal.transform.position);
            Vector3 direction = metal.transform.position - transform.position;
            Vector2 magneticForce = -Utility.Instance.CalculateMagneticForce(distance, direction);
            magnetRb.AddForce(magneticForce.x, 0, magneticForce.y, ForceMode.Force);
        }

        foreach (GameObject magnetGameObject in GameController.Instance.magnets)
        {
            Magnet magnet;
            if (magnetGameObject.TryGetComponent(out magnet) && magnet != this)
            {
                float distance = Vector3.Distance(transform.position, magnetGameObject.transform.position);
                Vector3 direction = (magnet.polarization != polarization ? 1 : -1) * (magnetGameObject.transform.position - transform.position);
                Vector2 magneticForce = Utility.Instance.CalculateMagneticForce(distance, direction) * magnetRb.mass;
                magnetRb.AddForce(magneticForce.x, 0, magneticForce.y, ForceMode.Force);
            }


        }

    }

    public void ChangeColor()
    {
        GetComponent<MeshRenderer>().material.color = polarization == Polarization.Negative ? Color.blue : Color.red;

    }


}
