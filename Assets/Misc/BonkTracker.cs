using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

public class BonkTracker : MonoBehaviour
{
	[SerializeField]
	[SuppressMessage("Style", "IDE0044")]
	private UnityEvent<string> OnBonk = new UnityEvent<string>();

	private readonly Queue<int> bonkOrder = new Queue<int>();

	[SuppressMessage("CodeQuality", "IDE0051")]
	private void OnCollisionEnter(Collision collision)
	{
		switch (collision.collider.tag)
		{
			case "One":
				AddBonk(1);
				break;

			case "Two":
				AddBonk(2);
				break;

			case "Three":
				AddBonk(3);
				break;
		}
	}

	private void AddBonk(int bonk)
	{
		bonkOrder.Enqueue(bonk);

		if (bonkOrder.Count > 3)
		{
			bonkOrder.Dequeue();
		}

		OnBonk?.Invoke(string.Join("", bonkOrder));
	}
}
