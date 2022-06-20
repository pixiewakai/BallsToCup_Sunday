using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour {
	private int _ballCount;

	private readonly List<Collider> _colliderCount = new List<Collider>();

	private void OnTriggerEnter(Collider other) {
		if (!_colliderCount.Contains(other) && other.gameObject.layer == LayerMask.NameToLayer("Ball")) {
			_ballCount++;
			_colliderCount.Add(other);
			GameManager.Instance.CupBallAdded();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (_colliderCount.Contains(other)) {
			_ballCount--;
			_colliderCount.Remove(other);
			GameManager.Instance.CupBallRemoved();
		}
	}
}