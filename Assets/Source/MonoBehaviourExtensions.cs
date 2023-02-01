using UnityEngine;
using UnityEngine.Events;

public static class MonoBehaviourExtensions
{
	public static void SafeStopCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine, UnityEvent coroutineStopped = null)
	{
		if (coroutine != null)
		{
			monoBehaviour.StopCoroutine(coroutine);
			coroutine = null;
			coroutineStopped?.Invoke();
		}
	}
}
