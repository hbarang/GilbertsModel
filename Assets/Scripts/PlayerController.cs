using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;

    Rigidbody draggedObject = null;
    int layerMask;

    void Start()
    {
        layerMask = LayerMask.GetMask(Utility.Instance.DragTargetsLayer);
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            SendRaycastFromInput(Input.touches[0].position);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                SendRaycastFromInput(Input.mousePosition);
            }
        }

    }

    void FixedUpdate()
    {

        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch currentTouch = Input.touches[0];
                if (currentTouch.phase == TouchPhase.Moved && draggedObject != null)
                {
                    AddForceFromInput(currentTouch.position);
                }
                else if (currentTouch.phase == TouchPhase.Ended)
                {
                    draggedObject = null;
                }
            }
        }
        else
        {

            if (Input.GetMouseButton(0) && draggedObject != null)
            {
                AddForceFromInput(Input.mousePosition);
            }
            else
            {
                draggedObject = null;
            }
        }

    }

    void SendRaycastFromInput(Vector3 input)
    {
        Ray ray = Camera.main.ScreenPointToRay(input);
        RaycastHit hit;
        Vector3 hitMousePosition = Camera.main.ScreenToWorldPoint(input);

        if (Physics.Raycast(ray, out hit, layerMask))
        {

            draggedObject = hit.collider.attachedRigidbody;

        }
    }

    void AddForceFromInput(Vector3 input)
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(input);

        if (targetPosition.x != draggedObject.transform.position.x && targetPosition.z != draggedObject.transform.position.z)
        {
            Vector2 differenceVector = new Vector2(targetPosition.x - draggedObject.transform.position.x, targetPosition.z - draggedObject.transform.position.z).normalized * draggedObject.mass * movementSpeed *Time.fixedDeltaTime;
            draggedObject.AddForce(differenceVector.x, 0f, differenceVector.y, ForceMode.Force);
        }
    }
    
}
