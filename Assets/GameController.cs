using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Transform polarizationChangerNegative;
    public Transform polarizationChangerPositive;
    [System.NonSerialized]
    public GameObject[] magnets;

    [System.NonSerialized]
    public GameObject[] metals;

    public static GameController Instance;
    public GameObject objectHolder;

    int objectCount = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Boundaries.Instance.OnScreenBoundsChangeEvent += AdjustPolarizerPosition;
        magnets = Utility.Instance.GetMagnets();
        metals = Utility.Instance.GetMetals();
        AdjustPolarizerPosition();
    }

    void Update()
    {
        if (objectHolder.transform.childCount != objectCount)
        {
            magnets = Utility.Instance.GetMagnets();
            metals = Utility.Instance.GetMetals();
        }
    }
    
    void AdjustPolarizerPosition()
    {
        polarizationChangerNegative.position = new Vector3(Boundaries.Instance.ScreenBounds.max.x, 0.1f, -Boundaries.Instance.ScreenBounds.extents.y);
        polarizationChangerPositive.position = new Vector3(Boundaries.Instance.ScreenBounds.min.x, 0.1f, Boundaries.Instance.ScreenBounds.extents.y);
    }

}
