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
    [SerializeField]
    GameObject metalPrefab;
    [SerializeField]
    GameObject magnetPrefab;
    Vector3 instantiatePosition;

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
        instantiatePosition = new Vector3(Boundaries.Instance.ScreenBounds.center.x, 0.2f, Boundaries.Instance.ScreenBounds.center.y);
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

    void AddHelper(ObjectType type)
    {
        float randomXValue = Random.Range(Boundaries.Instance.ScreenBounds.min.x, Boundaries.Instance.ScreenBounds.max.x);
        float randomZValue = Random.Range(-Boundaries.Instance.ScreenBounds.extents.y, Boundaries.Instance.ScreenBounds.extents.y);
        instantiatePosition = new Vector3(randomXValue, 0.2f, randomZValue);
        GameObject gameObject;
        gameObject = Instantiate(type == ObjectType.Magnet ? magnetPrefab : metalPrefab, instantiatePosition, Quaternion.identity);
    }

    public void AddMetal()
    {
        AddHelper(ObjectType.Metal);
    }

    public void AddMagnet()
    {
        AddHelper(ObjectType.Magnet);
    }


}

enum ObjectType
{
    Magnet,
    Metal
}
