using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public string DraggableTag = "Draggable";
    public string DragTargetsLayer = "DragTargets";
    public string MetalTag = "Metal";
    public float magneticForceConstant;
    public static Utility Instance;
    
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }

    }

    public Vector2 CalculateMagneticForce(float distance, Vector3 direction)
    {
        return new Vector2(MagneticForceCalculateHelper(distance, direction.x), MagneticForceCalculateHelper(distance, direction.z));
    }

    private float MagneticForceCalculateHelper(float distance, float direction)
    {
        return direction * magneticForceConstant / Mathf.Pow(distance, 2);
    }

    public GameObject[] GetMagnets()
    {
        return GameObject.FindGameObjectsWithTag(DraggableTag);
    }

    public GameObject[] GetMetals()
    {
        return GameObject.FindGameObjectsWithTag(MetalTag);
    }


}
