using System;
using UnityEngine;

public class TubeRotater : MonoBehaviour {
	private Vector2    _startPos;
	private Vector2    _currentPosition;
	private float      _resultingValueFromInput;
	private Quaternion _quaternionRotateFrom;
	private Quaternion _quaternionRotateTo;

	private float _movementAmount;

	private float _screenHeight;
	private float _screenWidth;

	private int _xRotationDirection = 1;
	private int _yRotationDirection = 1;

	private void Awake() {
		_screenHeight = Screen.height / 2f;
		_screenWidth = Screen.width / 2f;
	}

	private void Update() {
		if (GameManager.Instance.GameState != GameState.Playing) return;
		
		if (Input.GetMouseButtonDown(0)) {
			_startPos = Input.mousePosition;
		}

		if (Input.GetMouseButton(0)) {
			_currentPosition = Input.mousePosition;
			var currentMovement = _currentPosition - _startPos;

			if (_currentPosition.y < _screenHeight) {
				_xRotationDirection = 1;
			}

			if (_currentPosition.y > _screenHeight) {
				_xRotationDirection = -1;
			}

			if (_currentPosition.x < _screenWidth) {
				_yRotationDirection = 1;
			}

			if (_currentPosition.x > _screenWidth) {
				_yRotationDirection = -1;
			}


			if (currentMovement.x > 0) {
				_movementAmount = .1f * _xRotationDirection;
			} else if (currentMovement.x < 0) {
				_movementAmount = -.1f * _xRotationDirection;
			} else {
				if (currentMovement.y < 0) {
					_movementAmount = .1f * _yRotationDirection;
				} else if (currentMovement.y > 0) {
					_movementAmount = -.1f * _yRotationDirection;
				} else {
					_movementAmount = 0;
				}
			}

			_startPos = _currentPosition;
		}

		if (Input.GetMouseButtonUp(0)) {
			_movementAmount = 0;
		}
		
		_resultingValueFromInput += _movementAmount * GameManager.Instance.GameSettings.RotationSpeed;
		_quaternionRotateFrom = transform.rotation;
		_quaternionRotateTo = Quaternion.Euler(0, 0, _resultingValueFromInput);

		transform.rotation =
			Quaternion.Lerp(_quaternionRotateFrom, _quaternionRotateTo,
			                Time.deltaTime * GameManager.Instance.GameSettings.RotationSmoothness);
	}
	
}