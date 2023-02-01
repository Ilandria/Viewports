using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class ViewportRaycaster : MonoBehaviour
{
	[SerializeField]
	private LayerMask mask;

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent<Transform, Vector3> OnRaycastHit = new UnityEvent<Transform, Vector3>();

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent<Vector3> OnRaycast = new UnityEvent<Vector3>();

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent OnViewportExit = new UnityEvent();

	private Camera viewportCamera = null;

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Start()
	{
		viewportCamera = GetComponent<Camera>();
	}

	public void StartTracking()
	{
		// Not currently used.
	}

	public void Track(Vector3 position)
	{
		// Todo: fix the math here so it lines up properly.
		Ray ray = viewportCamera.ViewportPointToRay(position);

		OnRaycast?.Invoke(ray.origin);

		// This could check for a specific "grabbable" component on a game object, which would be more performant on a larger scale,
		// but this approach allows the system to work with any object listening to the events instead of only specific types.
		// Definitely something to re-think for performance if this project was larger scale.
		if(Physics.Raycast(ray, out RaycastHit hitInfo, viewportCamera.farClipPlane - viewportCamera.nearClipPlane, mask.value))
		{
			OnRaycastHit?.Invoke(hitInfo.transform, ray.origin);
		}
	}

	public void StopTracking()
	{
		OnViewportExit?.Invoke();
	}
}
