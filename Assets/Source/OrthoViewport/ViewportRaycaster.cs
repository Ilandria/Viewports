using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ViewportRaycaster : MonoBehaviour
{
	private Camera camera = null;

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Start()
	{
		camera = GetComponent<Camera>();
	}

	public void StartTracking()
	{
	}

	public void Track(Vector3 position)
	{
		// Todo: fix the math here so it lines up properly.
		Ray ray = camera.ViewportPointToRay(position);
		Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100.0f);
	}

	public void StopTracking()
	{
	}
}
