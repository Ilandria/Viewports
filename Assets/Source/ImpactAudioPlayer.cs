using System;
using System.Collections.Generic;
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
	private List<SoundMap> soundEffects = new List<SoundMap>();

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		foreach (SoundMap soundMap in soundEffects)
		{
			if (string.IsNullOrWhiteSpace(soundMap.objectTag) || collision.collider.tag.Equals(soundMap.objectTag))
			{
				audioSource.PlayOneShot(soundMap.audioClip);
			}
		}
	}
}
