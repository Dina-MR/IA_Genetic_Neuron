using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenWinState : MonoBehaviour
{
    public bool hasReachedGoal; // indique si le poulet a franchi la ligne d'arrivée
    public bool isAlive;
    private float _deathDistance;
    [SerializeField] private float _distanceBetweenGroundAndDeath; // distance d'écart entre le parcours et la zone d'élimination
    [SerializeField] private float _groundAltitude; // altitude de la zone de parcours

    private void Start()
    {
        hasReachedGoal = false;
        isAlive = true;
        _deathDistance = _groundAltitude - _distanceBetweenGroundAndDeath;
    }

    private void Update()
    {
        if(isAlive)
            CheckAltitude();
    }

    /// <summary>
    /// Vérification de l'altitude. Le poulet est considéré mort s'il tombe du plateau
    /// </summary>
    private void CheckAltitude()
    {
        if(transform.position.y <= _deathDistance && isAlive)
            isAlive = false;
    }
}
