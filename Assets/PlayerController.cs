using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    Vector2 screenDragStartPosition;
    Rigidbody draggedObject = null;
    int layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask(Utility.Instance.DragTargetsLayer);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layerMask))
            {

                if (hit.collider.CompareTag(Utility.Instance.DraggableTag))
                {
                    draggedObject = hit.collider.attachedRigidbody;
                }

            }
        }
    }

    void FixedUpdate()
    {


        if (Input.GetMouseButton(0) && draggedObject != null)
        {
            Vector3 targetMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (targetMousePosition.x != draggedObject.transform.position.x && targetMousePosition.z != draggedObject.transform.position.z)
            {
                Vector2 differenceVector = new Vector2(targetMousePosition.x - draggedObject.transform.position.x, targetMousePosition.z - draggedObject.transform.position.z).normalized * movementSpeed;
                draggedObject.AddForce(differenceVector.x, 0f, differenceVector.y, ForceMode.Force);
            }
        }
        else
        {
            draggedObject = null;
        }

    }


}
