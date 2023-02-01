using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonFlightController : MonoBehaviour
{
	[SerializeField]
	private float movementForce = 5.0f;

	private new Rigidbody rigidbody = null;
	private Vector3 rawInput = Vector3.zero;
	private bool controlsEnabled = false;

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void FixedUpdate()
	{
		if (!controlsEnabled)
		{
			return;
		}

		rawInput.Normalize();
		Vector3 movementDirection = transform.TransformDirection(rawInput);
		rigidbody.AddForce(movementDirection * movementForce, ForceMode.Force);
	}

	public void Horizontal(InputAction.CallbackContext context)
	{
		Vector2 horizontal = context.ReadValue<Vector2>();
		rawInput.z = horizontal.y;
		rawInput.x = horizontal.x;
	}

	public void Vertical(InputAction.CallbackContext context)
	{
		rawInput.y = context.ReadValue<float>();
	}

	public void ToggleControls(InputAction.CallbackContext context)
	{
		controlsEnabled = context.ReadValue<float>() > 0.5f;
	}
}
