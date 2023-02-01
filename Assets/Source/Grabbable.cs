using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Grabbable : MonoBehaviour
{
	private Vector3 grabOffset = Vector3.zero;
	private Coroutine checkForRelease = null;

	public void Hover(Transform hoveredTransform, Vector3 worldPosition)
	{
		if (checkForRelease != null || hoveredTransform != transform)
		{
			return;
		}

		if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
		{
			grabOffset = transform.position - worldPosition;
			checkForRelease = StartCoroutine(CheckForRelease());
		}
	}

	public void Move(Vector3 worldPosition)
	{
		if (checkForRelease != null)
		{
			transform.position = worldPosition + grabOffset;
		}
	}

	public void Release()
	{
		this.SafeStopCoroutine(ref checkForRelease);
		grabOffset = Vector3.zero;
	}

	private IEnumerator CheckForRelease()
	{
		while (true)
		{
			if (Input.GetMouseButtonUp((int)MouseButton.LeftMouse))
			{
				Release();
			}

			yield return null;
		}
	}
}
