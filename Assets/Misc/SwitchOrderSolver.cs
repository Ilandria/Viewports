using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SwitchOrderSolver : MonoBehaviour
{
	[SerializeField]
	private string solution = "123";

	[SerializeField]
	private string hiddenSolution = "231";

	[SerializeField]
	private AudioClip successAudio = null;

	[SerializeField]
	private AudioClip failureAudio = null;

	[SerializeField]
	private Transform trueEndWarp = null;

	[SerializeField]
	private float trueEndingDelay = 1.5f;

	[SerializeField]
	private UnityEvent OnClearSolution = new UnityEvent();

	[SerializeField]
	private UnityEvent OnEnding = new UnityEvent();

	[SerializeField]
	private UnityEvent OnTrueEnding = new UnityEvent();

	[SerializeField]
	private UnityEvent<Transform> OnTrueEndingDelayed = new UnityEvent<Transform>();

	private AudioSource solutionAudioSource = null;
	private bool hasSolvedPuzzle = false;

	private void Start()
	{
		solutionAudioSource = GetComponent<AudioSource>();
	}

	public void OnSwitchHit(string switchOrder)
	{
		if (switchOrder.Length != 3)
		{
			return;
		}

		if (!hasSolvedPuzzle)
		{
			if (switchOrder.Equals(solution))
			{
				hasSolvedPuzzle = true;
				solutionAudioSource.PlayOneShot(successAudio);
				OnEnding?.Invoke();
				OnClearSolution?.Invoke();
			}
			else
			{
				solutionAudioSource.PlayOneShot(failureAudio);
				OnClearSolution?.Invoke();
			}
		}
		else
		{
			if (switchOrder.Equals(hiddenSolution))
			{
				OnTrueEnding?.Invoke();
				StartCoroutine(DelayedTrueEnding());
				OnClearSolution?.Invoke();
			}
			else
			{
				OnClearSolution?.Invoke();
			}
		}		
	}

	private IEnumerator DelayedTrueEnding()
	{
		yield return new WaitForSeconds(trueEndingDelay);
		OnTrueEndingDelayed?.Invoke(trueEndWarp);
	}
}
