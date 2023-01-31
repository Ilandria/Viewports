using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ViewportMouseTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent OnViewportEnter = new UnityEvent();

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent<Vector3> OnViewportHover = new UnityEvent<Vector3>();

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent OnViewportExit = new UnityEvent();

	private Coroutine cursorRoutine = null;

	public void OnPointerEnter(PointerEventData eventData)
	{
		StartTracking();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StopTracking();
	}

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void OnDisable()
	{
		StopTracking();
	}

	private void StartTracking()
	{
		StopTracking();

		OnViewportEnter?.Invoke();
		cursorRoutine = StartCoroutine(TrackCursor());
	}

	private void StopTracking()
	{
		if (cursorRoutine != null)
		{
			StopCoroutine(cursorRoutine);
			cursorRoutine = null;
			OnViewportExit?.Invoke();
		}
	}

	private IEnumerator TrackCursor()
	{
		while (true)
		{
			// Calculate pixel coordinates of viewport.
			Vector3[] corners = new Vector3[4];
			RectTransform viewportTransform = GetComponent<RectTransform>();
			viewportTransform.GetWorldCorners(corners);
			Rect viewportRect = new Rect(corners[0], corners[2] - corners[0]);

			// Calculate 0-1 position of mouse within viewport.
			Vector3 mousePos = Input.mousePosition;
			mousePos.x -= viewportRect.x;
			mousePos.y -= viewportRect.y;
			mousePos.x /= viewportRect.width;
			mousePos.y /= viewportRect.height;

			// Tell listeners where we be pointin'!
			OnViewportHover?.Invoke(mousePos);

			yield return null;
		}
	}
}
