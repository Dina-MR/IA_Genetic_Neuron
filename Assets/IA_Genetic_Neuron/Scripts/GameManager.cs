using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager of the overall game.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Sub managers")]
    [SerializeField] private ChickenGenerator _chickenGenerator;
    [SerializeField] private ChickenManager _chickenManager;
    [SerializeField] private WinnersChecker _winnersChecker;
    [SerializeField] private UIManager _UIManager;

    [Header("Screens")]
    [SerializeField] private GameObject _transitionScreen;

    [Header("Others")]
    [SerializeField] private CameraMovement _cameraMovement;

    [SerializeField] private GameState _currentState;
    [SerializeField] private bool _raceStarted;
    private bool _isReadyToStart;

    // Start is called before the first frame update
    void Start()
    {
        // We need to desactivate the transition canvas first
        _transitionScreen.SetActive(false);
        // We start the simulation with the randomly generated first generation
        _currentState = GameState.SIMULATION;
        _raceStarted = true;
        _isReadyToStart = true;
        _chickenGenerator.StartSimulation();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
        UpdateGameState();
    }

    private void CheckGameState()
    {
        // Each race stops when there're only the winners remaining on the field (or if all chicken lose the race)
        if(_chickenManager.currentChickenAmount == _winnersChecker.winnersCount || _chickenManager.currentChickenAmount == 0)
        {
            _currentState = GameState.TRANSITION;
            _raceStarted = false;
            _isReadyToStart = false;
        }
        // We're about to begin a new race, with the next generation
        else if(_currentState != GameState.SIMULATION && _isReadyToStart)
        {
            _currentState = GameState.SIMULATION;
        }
    }

    private void UpdateGameState()
    {
        if(_currentState == GameState.TRANSITION)
        {
            _transitionScreen.SetActive(true);
            _chickenManager.SetupNextGeneration();
            _winnersChecker.winnersCount = 0;
            _isReadyToStart = true;
        }
        else if(_currentState == GameState.SIMULATION && !_raceStarted)
        {
            _transitionScreen.SetActive(false);
            _cameraMovement.ResetPosition();
            _UIManager.UpdateUI();
            _raceStarted = true;
        }
    }

    enum GameState
    {
        SIMULATION,
        TRANSITION
    }
}
