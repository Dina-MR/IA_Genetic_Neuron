using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the genetical data along with all the different event that impact it (crossbreeding, mutation...)
/// </summary>
public class ChickenGenetic : MonoBehaviour
{
    public float _fitnessScore; // Fitness score, used to select the best chicken
    [SerializeField] private float _mutationRate = 0.01f; // The chance a chicken will mutate
    private bool _canMutate; // The ability or not for a chicken to mutate
    [HideInInspector] public Queue<string> pathDNA; // The path followed by the chicken is considered to be its DNA
    private ChickenMove _chickenMoveData;

    // Start is called before the first frame update
    void Start()
    {
        _chickenMoveData = gameObject.GetComponent<ChickenMove>();
        DetermineMutation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Calculation of the fitness score
    /// </summary>
    /// <param name="victoryMultiplier">Bonus given to the chicken who won the rave. By default, its value is equal to 1 (for those who lose).</param>
    public void CalculateFitnessScore(int victoryMultiplier = 1)
    {
        float distanceTraveled = _chickenMoveData.distanceTravelled;
        float totalTime = _chickenMoveData.timer;
        _fitnessScore = victoryMultiplier * (1 / distanceTraveled + 1 / totalTime + 1);
    }

    /// <summary>
    /// Get the DNA of the chicken after the race is over
    /// </summary>
    public void GetPathDNA()
    {
        pathDNA = _chickenMoveData._path;
    }

    public Queue<string> Crossbreed(GameObject partner)
    {
        Queue<string> newDNA = new Queue<string>();
        Queue<string> partnerDNA = partner.GetComponent<ChickenGenetic>().pathDNA;
        return newDNA;
    }

    public void DetermineMutation()
    {
        float randomValueForMutation = Random.Range(0, 1);
        if (randomValueForMutation <= _mutationRate)
            _canMutate = true;
        else
            _canMutate = false;
    }
}
