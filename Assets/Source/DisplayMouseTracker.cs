using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

public class DisplayMouseTracker : MonoBehaviour
{
	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent<Vector3> OnMouseTrack = new UnityEvent<Vector3>();

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.x /= Screen.width;
		mousePosition.y /= Screen.height;

		OnMouseTrack?.Invoke(mousePosition);
	}
}
