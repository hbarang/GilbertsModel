using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Transform polarizationChangerNegative;
    public Transform polarizationChangerPositive;

    void Start()
    {
        Boundaries.Instance.OnScreenBoundsChangeEvent += AdjustPolarizerPosition;

        AdjustPolarizerPosition();

    }

    void AdjustPolarizerPosition()
    {
        polarizationChangerNegative.position = new Vector3(Boundaries.Instance.ScreenBounds.max.x, 0.1f, -Boundaries.Instance.ScreenBounds.extents.y);
        polarizationChangerPositive.position = new Vector3(Boundaries.Instance.ScreenBounds.min.x, 0.1f, Boundaries.Instance.ScreenBounds.extents.y);
    }

}
