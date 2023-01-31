using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class ViewportPanelScaler : MonoBehaviour
{
	[SerializeField]
	private float panelWidth = 100.0f;

	private RectTransform rectTransform = null;

	// Hacky way around the warning spam from updating values in OnValidate. Other solution is to make a custom inspector, but that's overkill.
	[SuppressMessage("CodeQuality", "IDE0051")]
	private void OnValidate() => Invoke("Resize", 0.0f);

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Awake() => Resize();

	private void Resize()
	{
		if (rectTransform == null)
		{
			rectTransform = GetComponent<RectTransform>();
		}

		rectTransform.sizeDelta = new Vector2(panelWidth, panelWidth * rectTransform.childCount);
	}
}
