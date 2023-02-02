using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ImpactAudioPlayer : MonoBehaviour
{
	[Serializable]
	public struct SoundMap
	{
		public string objectTag;
		public AudioClip audioClip;
	}

	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private List<SoundMap> soundEffects = new List<SoundMap>();

	private AudioSource audioSource;

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void OnCollisionEnter(Collision collision)
	{
		foreach (SoundMap soundMap in soundEffects)
		{
			if (collision.collider.tag.Equals(soundMap.objectTag))
			{
				audioSource.clip = soundMap.audioClip;
				audioSource.Play();
			}
		}
	}
}
