using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MovementAudioPlayer : MonoBehaviour
{
	[SerializeField]
	private float silentVelocity = 0.1f;

	[SerializeField]
	private float loudVelocity = 5.0f;

	[SerializeField]
	private new Rigidbody rigidbody;

	private AudioSource audioSource;
	private float maxVolume;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		maxVolume = audioSource.volume;
	}

	private void Update()
	{
		audioSource.volume = Mathf.InverseLerp(silentVelocity, loudVelocity, rigidbody.velocity.sqrMagnitude) * maxVolume;
	}
}
