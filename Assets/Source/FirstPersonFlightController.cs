using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonFlightController : MonoBehaviour
{
	[Header("Input Sensitivity")]
	[SerializeField]
	private float movementForce = 5.0f;

	[SerializeField]
	private float lookForce = 2.0f;

	[SerializeField]
	private float rollForce = 10.0f;

	#region Legacy Input for WebGL Support

	// We're hiding these for non-webgl builds, but they still exist. If we remove them fully then the bindings get reset when you switch platforms.
#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[Header("Legacy Input for WebGL Builds")]
	[SerializeField]
	private KeyCode forward = KeyCode.W;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private KeyCode backward = KeyCode.S;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private KeyCode left = KeyCode.A;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private KeyCode right = KeyCode.D;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private KeyCode rollLeft = KeyCode.Q;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private KeyCode rollRight = KeyCode.E;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private KeyCode up = KeyCode.Space;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private KeyCode down = KeyCode.LeftControl;

#if !UNITY_WEBGL
	[HideInInspector]
#endif
	[SerializeField]
	private int cameraMouseButton = 1;

	#endregion

	private new Rigidbody rigidbody = null;
	private Vector3 rawMovementInput = Vector3.zero;
	private Vector3 rawLookInput = Vector3.zero;
	private bool controlsEnabled = false;

	#region Legacy Input for WebGL Support

	// To track delta for looking around.
	private Vector2 previousMousePosition = Vector2.zero;

	#endregion

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

	#region Legacy Input for WebGL Support

	// Old gross polling system because WebGL doesn't play nice with the new event-driven input system.
#if UNITY_WEBGL
	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Update()
	{
		rawMovementInput = Vector3.zero;
		rawLookInput = Vector3.zero;

		controlsEnabled = Input.GetMouseButton(cameraMouseButton);
		Cursor.visible = !controlsEnabled;

		rawMovementInput.z += Input.GetKey(forward) ? 1.0f : 0.0f;
		rawMovementInput.z -= Input.GetKey(backward) ? 1.0f : 0.0f;
		rawMovementInput.x += Input.GetKey(right) ? 1.0f : 0.0f;
		rawMovementInput.x -= Input.GetKey(left) ? 1.0f : 0.0f;
		rawMovementInput.y += Input.GetKey(up) ? 1.0f : 0.0f;
		rawMovementInput.y -= Input.GetKey(down) ? 1.0f : 0.0f;

		Vector2 mousePosition = Input.mousePosition;
		rawLookInput.x = (previousMousePosition.y - mousePosition.y) * lookForce;
		rawLookInput.y = (mousePosition.x - previousMousePosition.x) * lookForce;
		previousMousePosition = mousePosition;

		rawLookInput.z += Input.GetKey(rollLeft) ? rollForce : 0.0f;
		rawLookInput.z -= Input.GetKey(rollRight) ? rollForce : 0.0f;
	}
#endif

	#endregion

	public void Horizontal(InputAction.CallbackContext context)
	{
#if UNITY_STANDALONE_WIN
		Vector2 horizontal = context.ReadValue<Vector2>();
		rawMovementInput.z = horizontal.y;
		rawMovementInput.x = horizontal.x;
#endif
	}

	public void Vertical(InputAction.CallbackContext context)
	{
#if UNITY_STANDALONE_WIN
		rawMovementInput.y = context.ReadValue<float>();
#endif
	}

	public void Look(InputAction.CallbackContext context)
	{
#if UNITY_STANDALONE_WIN
		Vector2 look = context.ReadValue<Vector2>() * lookForce;
		rawLookInput.x = -look.y;
		rawLookInput.y = look.x;
#endif
	}

	public void Roll(InputAction.CallbackContext context)
	{
#if UNITY_STANDALONE_WIN
		rawLookInput.z = -context.ReadValue<float>() * rollForce;
#endif
	}

	public void ToggleControls(InputAction.CallbackContext context)
	{
#if UNITY_STANDALONE_WIN
		controlsEnabled = context.ReadValue<float>() > 0.5f;
		Cursor.visible = !controlsEnabled;
#endif
	}
}
