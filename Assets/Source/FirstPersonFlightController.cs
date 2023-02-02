using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonFlightController : MonoBehaviour
{
	[SerializeField]
	private float movementForce = 5.0f;

	[SerializeField]
	private float lookForce = 2.0f;

	[SerializeField]
	private float rollForce = 10.0f;

	private new Rigidbody rigidbody = null;
	private Vector3 rawMovementInput = Vector3.zero;
	private Vector3 rawLookInput = Vector3.zero;
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

		rawMovementInput.Normalize();
		Vector3 movementDirection = transform.TransformDirection(rawMovementInput);

		rigidbody.AddTorque(transform.TransformVector(rawLookInput), ForceMode.Force);
		rigidbody.AddForce(movementDirection * movementForce, ForceMode.Force);
	}

	public void Horizontal(InputAction.CallbackContext context)
	{
		Vector2 horizontal = context.ReadValue<Vector2>();
		rawMovementInput.z = horizontal.y;
		rawMovementInput.x = horizontal.x;
	}

	public void Vertical(InputAction.CallbackContext context)
	{
		rawMovementInput.y = context.ReadValue<float>();
	}

	public void Look(InputAction.CallbackContext context)
	{
		Vector2 look = context.ReadValue<Vector2>() * lookForce;
		rawLookInput.x = -look.y;
		rawLookInput.y = look.x;
	}
	
	public void Roll(InputAction.CallbackContext context)
	{
		rawLookInput.z = -context.ReadValue<float>() * rollForce;
	}

	public void ToggleControls(InputAction.CallbackContext context)
	{
		controlsEnabled = context.ReadValue<float>() > 0.5f;
	}
}
