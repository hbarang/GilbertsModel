using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{

    Bounds _screenBounds;
    public Bounds ScreenBounds
    {
        get
        {
            return _screenBounds;
        }
        set
        {
            _screenBounds = value;

            if (OnScreenBoundsChangeEvent != null)
            {
                OnScreenBoundsChangeEvent();
            }
        }
    }

    public delegate void OnScreenBoundsChange();
    public event OnScreenBoundsChange OnScreenBoundsChangeEvent;
    public static Boundaries Instance;

    private float originalCameraAspect;

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

        originalCameraAspect = Camera.main.aspect;
        ScreenBounds = OrthographicBounds();

    }


    private void Update()
    {

        if (originalCameraAspect != Camera.main.aspect)
        {

            ScreenBounds = OrthographicBounds();
            originalCameraAspect = Camera.main.aspect;

        }

    }

    private Bounds OrthographicBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        Bounds bounds = new Bounds(
            Camera.main.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }



}