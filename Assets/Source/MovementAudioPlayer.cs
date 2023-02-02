using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MovementAudioPlayer : MonoBehaviour
{
	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private float silentVelocity = 0.1f;

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private float loudVelocity = 5.0f;

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private new Rigidbody rigidbody;

	private AudioSource audioSource;
	private float maxVolume;

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		maxVolume = audioSource.volume;
	}

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Update()
	{
		audioSource.volume = Mathf.InverseLerp(silentVelocity, loudVelocity, rigidbody.velocity.sqrMagnitude) * maxVolume;
	}
}
