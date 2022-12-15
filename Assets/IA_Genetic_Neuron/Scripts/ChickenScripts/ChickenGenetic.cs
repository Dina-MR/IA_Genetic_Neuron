using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// This class handles the genetical data along with all the different event that impact it (crossbreeding, mutation...)
/// </summary>
public class ChickenGenetic : MonoBehaviour
{
    public float _fitnessScore; // Fitness score, used to select the best chicken
    [SerializeField] private float _mutationRate = 0.01f; // The chance a chicken will mutate
    private bool _canMutate; // The ability or not for a chicken to mutate
    [HideInInspector] public List<string> pathDNA; // The path followed by the chicken is considered to be its DNA
    [HideInInspector] public Queue<string> currentPathDNA; // The current path generated. It'll reset itself when it gets empty
    private ChickenMove _chickenMoveData;

    // Start is called before the first frame update
    void Start()
    {
        pathDNA = new List<string>();
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
    //public void GetPathDNA()
    //{
    //    currentPathDNA = _chickenMoveData._path;
    //}

    public List<string> Crossbreed(GameObject partner)
    {
        List<string> newDNA = new List<string>();
        List<string> partnerDNA = partner.GetComponent<ChickenGenetic>().pathDNA;
        int shortestDNASize = Math.Min(pathDNA.Count, partnerDNA.Count);
        int randomDNAPickerLimit = UnityEngine.Random.Range(0, shortestDNASize); // we randomly choose which DNA will get its fragment picked, for each position of the new DNA
        for(int i = 0; i < shortestDNASize; i++)
        {
            if (i <= randomDNAPickerLimit)
                newDNA.Add(pathDNA[i]);
            else
                newDNA.Add(partnerDNA[i]);
        }
        return newDNA;
    }

    public void DetermineMutation()
    {
        float randomValueForMutation = UnityEngine.Random.Range(0, 1);
        if (randomValueForMutation <= _mutationRate)
            _canMutate = true;
        else
            _canMutate = false;
    }
}
