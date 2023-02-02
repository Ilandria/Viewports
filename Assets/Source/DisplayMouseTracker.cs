using UnityEngine;
using UnityEngine.Events;

public class DisplayMouseTracker : MonoBehaviour
{
	[SerializeField]
	private UnityEvent<Vector3> OnMouseTrack = new UnityEvent<Vector3>();

	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.x /= Screen.width;
		mousePosition.y /= Screen.height;

		OnMouseTrack?.Invoke(mousePosition);
	}
}
