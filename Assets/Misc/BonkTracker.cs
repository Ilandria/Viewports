using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BonkTracker : MonoBehaviour
{
	[SerializeField]
	private UnityEvent<string> OnBonk = new UnityEvent<string>();

	private readonly Queue<int> bonkOrder = new Queue<int>();

	public void ClearBonks()
	{
		bonkOrder.Clear();
		NotifyOfBonk();
	}

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

		NotifyOfBonk();
	}

	private void NotifyOfBonk()
	{
		OnBonk?.Invoke(string.Join("", bonkOrder));
	}
}
