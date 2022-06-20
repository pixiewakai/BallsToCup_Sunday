using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour {
	public int BallCount;

	private readonly List<Collider> _colliderCount = new List<Collider>();

	private void OnTriggerEnter(Collider other) {
		if (!_colliderCount.Contains(other) && other.gameObject.layer == LayerMask.NameToLayer("Ball")) {
			BallCount++;
			_colliderCount.Add(other);
			GameManager.Instance.TubeBallAdded();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (_colliderCount.Contains(other)) {
			BallCount--;
			_colliderCount.Remove(other);
			GameManager.Instance.TubeBallRemoved();
		}
	}
}