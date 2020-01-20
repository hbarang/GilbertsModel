using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarizationChangerController : MonoBehaviour
{
    public Polarization polarization;

    void Start()
    {
        GetComponent<MeshRenderer>().material.color = polarization == Polarization.Negative ? Color.blue : Color.red;
    }

    void OnTriggerEnter(Collider other)
    {

        Magnet magnet;

        if (other.TryGetComponent<Magnet>(out magnet))
        {
            magnet.polarization = polarization;
            magnet.ChangeColor();
        }
    }

}
