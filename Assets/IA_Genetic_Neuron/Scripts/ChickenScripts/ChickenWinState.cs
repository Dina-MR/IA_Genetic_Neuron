using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenWinState : MonoBehaviour
{
    public bool hasReachedGoal; // indique si le poulet a franchi la ligne d'arriv�e
    public bool isAlive;
    private float _deathDistance;
    [SerializeField] private float _distanceBetweenGroundAndDeath; // distance d'�cart entre le parcours et la zone d'�limination
    [SerializeField] private float _groundAltitude; // altitude de la zone de parcours

    private ChickenManager _chickenManager; // manager de poulets
    private UIManager _uiManager; // UI

    private void Start()
    {
        hasReachedGoal = false;
        isAlive = true;
        _deathDistance = _groundAltitude - _distanceBetweenGroundAndDeath;
        // Acc�s au manager
        _chickenManager = GameObject.Find("ChickenManager").GetComponent<ChickenManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if(isAlive)
            CheckAltitude();
    }

    /// <summary>
    /// V�rification de l'altitude. Le poulet est consid�r� mort s'il tombe du plateau
    /// </summary>
    private void CheckAltitude()
    {
        if(transform.position.y <= _deathDistance && isAlive)
        {
            isAlive = false;
            _chickenManager.currentChickenAmount--;
            _uiManager.SetChickenCountUI();
        }
    }
}
