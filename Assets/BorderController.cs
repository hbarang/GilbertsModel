using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{


    public Transform[] borders;

    void Start()
    {

        AdjustPosition();
        Boundaries.Instance.OnScreenBoundsChangeEvent += AdjustPosition;

    }
    void AdjustPosition()
    {

        for (int i = 0; i < borders.Length; i++)
        {
            if (i < 2)
                HandleHorizontal(borders[i], i == 0);
            else
                HandleVertical(borders[i], i == 2);
        }

    }

    void HandleHorizontal(Transform border, bool isWest)
    {
        Vector3 newPosition = new Vector3((isWest ? 1 : -1) * Boundaries.Instance.ScreenBounds.min.x, border.position.y, Boundaries.Instance.ScreenBounds.center.y);
        border.position = newPosition;
    }
    void HandleVertical(Transform border, bool isNorth)
    {
        Vector3 newPosition = new Vector3(0, border.position.y, (isNorth ? 1 : -1) * Boundaries.Instance.ScreenBounds.extents.y);
        border.position = newPosition;
    }

    private void OnDestroy()
    {

        Boundaries.Instance.OnScreenBoundsChangeEvent -= AdjustPosition;

    }
}
