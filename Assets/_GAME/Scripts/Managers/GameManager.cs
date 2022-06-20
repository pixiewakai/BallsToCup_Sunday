using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager  Instance;
	
	public GameSettings GameSettings;
	public GameState    GameState { get; private set; }

	[SerializeField] private List<Level>     Levels;
	[SerializeField] private TextMeshProUGUI LevelText;
	[SerializeField] private TextMeshProUGUI BallCounterText;
	[SerializeField] private GameObject      FailUI;
	[SerializeField] private GameObject      SuccessUI;

	private int _tubeBallCount;
	private int _cupBallCount;

	private int _ballCountToWin;

	private int _levelIndex;

	private Level     _currentLevel;

	private void Awake() {
		Instance = this;
	}

	private void Start() {
		InitLevel();
	}

	private void InitLevel() {
		FailUI.SetActive(false);
		SuccessUI.SetActive(false);
		_tubeBallCount = 0;
		_cupBallCount = 0;
		_currentLevel = Instantiate(Levels[_levelIndex]);
		_ballCountToWin = _currentLevel.BallCountToWin;
		BallCounterText.text = $"0/{_ballCountToWin}";
		LevelText.text = $"LEVEL {_levelIndex + 1}";
		GameState = GameState.Playing;
	}

	public void TubeBallRemoved() {
		if (GameState != GameState.Playing) return;
		
		_tubeBallCount--;
		if (_tubeBallCount == 0) {
			Invoke(nameof(CheckForLevelEnd), 2f);
		}
	}

	public void TubeBallAdded() {
		_tubeBallCount++;
	}

	public void CupBallAdded() {
		if (GameState != GameState.Playing) return;
		
		_cupBallCount++;
		BallCounterText.text = $"{_cupBallCount}/{_ballCountToWin}";
		CheckForLevelEnd();
	}

	public void CupBallRemoved() {
		if (GameState != GameState.Playing) return;

		_cupBallCount--;
	}

	private void CheckForLevelEnd() {
		if (GameState != GameState.Playing) return;

		if (_cupBallCount >= _ballCountToWin) {
			SuccessUI.SetActive(true);
			GameState = GameState.Win;
			_levelIndex++;
			_levelIndex %= Levels.Count;
			return;
		}

		if (_tubeBallCount == 0 && _cupBallCount < _ballCountToWin) {
			GameState = GameState.Lose;
			GameState = GameState.Lose;
			FailUI.SetActive(true);
		}
	}

	public void RestartLevel() {
		StartCoroutine(RestartGame());
	}

	private IEnumerator RestartGame() {
		Destroy(_currentLevel.gameObject);

		yield return new WaitForEndOfFrame();

		InitLevel();
	}
}