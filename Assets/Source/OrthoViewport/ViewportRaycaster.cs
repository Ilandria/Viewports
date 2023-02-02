using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class ViewportRaycaster : MonoBehaviour
{
	[SerializeField]
	private LayerMask mask;

	[SerializeField]
	private UnityEvent<Transform, Vector3> OnRaycastHit = new UnityEvent<Transform, Vector3>();

	[SerializeField]
	private UnityEvent<Vector3> OnRaycast = new UnityEvent<Vector3>();

	[SerializeField]
	private UnityEvent OnViewportExit = new UnityEvent();

	private Camera viewportCamera = null;

	private void Start()
	{
		viewportCamera = GetComponent<Camera>();
	}

	public void StartTracking()
	{
		// Not currently used.
		OnViewportExit?.Invoke();
	}

	public void Track(Vector3 position)
	{
		Ray ray = viewportCamera.ViewportPointToRay(position);

		OnRaycast?.Invoke(ray.origin);

		if(Physics.Raycast(ray, out RaycastHit hitInfo, viewportCamera.farClipPlane - viewportCamera.nearClipPlane, mask.value))
		{
			OnRaycastHit?.Invoke(hitInfo.transform, ray.origin);
		}
		Debug.DrawLine(ray.origin, ray.origin + ray.direction * (viewportCamera.farClipPlane - viewportCamera.nearClipPlane));
	}

	public void StopTracking()
	{
		// This could check for a specific "grabbable" component on a game object, which would be more performant on a larger scale,
		// but this approach allows the system to work with any object listening to the events instead of only specific types.
		// Definitely something to re-think for performance if this project was larger scale.
		OnViewportExit?.Invoke();
	}
}
