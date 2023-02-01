using UnityEngine;
using UnityEngine.UIElements;

public class Grabbable : MonoBehaviour
{
    private bool isGrabbed = false;
    private Vector3 grabOffset = Vector3.zero;

    public void Hover(Transform hoveredTransform, Vector3 worldPosition)
    {
        if (isGrabbed || hoveredTransform != transform)
        {
            return;
        }

        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            isGrabbed = true;
            grabOffset = transform.position - worldPosition;
        }
    }

    public void Move(Vector3 worldPosition)
    {
        if (isGrabbed)
        {
            transform.position = worldPosition + grabOffset;
        }
    }

    public void Release()
    {
        isGrabbed = false;
    }

    public void Update()
    {
        if (isGrabbed && Input.GetMouseButtonUp((int)MouseButton.LeftMouse))
        {
            Release();
        }
    }
}
