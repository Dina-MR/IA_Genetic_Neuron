using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class ChickenManager : MonoBehaviour
{
    [HideInInspector] public int generationNumber = 1;
    [HideInInspector] public int currentChickenAmount; // Number of remaining chicken running
    [HideInInspector] public List<GameObject> chickenList; // List of all generated chicken
    private ChickenGenerator _generator;

    [Header("Winners parameters")]
    [SerializeField] private int _winnerBonus = 2;
    [SerializeField] private int _selectedChickenCount; // Number of chicken selected for breeding
    [SerializeField, Range(1, 2)] private int _childrenPerWinners = 2;
    private void Start()
    {
        _generator = gameObject.GetComponent<ChickenGenerator>();
    }

    /// <summary>
    /// Setting up the new generation
    /// </summary>
    public void SetupNextGeneration()
    {
        CalculateAllScores();
        SortByFitnessScores();
        chickenList = CrossbreedWinners(SelectWinners());
        generationNumber++;
        currentChickenAmount = chickenList.Count;
    }

    /// <summary>
    /// Calculation of all chicken's fitness score after the run is over
    /// </summary>
    public void CalculateAllScores()
    {
        Debug.Log("Calculating all fitness score...");
        foreach (var chicken in chickenList)
        {
            if(chicken.GetComponent<ChickenWinState>().hasReachedGoal)
                chicken.GetComponent<ChickenGenetic>().CalculateFitnessScore(_winnerBonus);
            else
                chicken.GetComponent<ChickenGenetic>().CalculateFitnessScore();
        }
    }

    /// <summary>
    /// Sort the chicken by their fitness score
    /// </summary>
    public void SortByFitnessScores()
    {
        chickenList.Sort(delegate(GameObject chickenA, GameObject chickenB)
        {
            return (chickenA.GetComponent<ChickenGenetic>()._fitnessScore).CompareTo(chickenB.GetComponent<ChickenGenetic>()._fitnessScore);
        });
    }

    /// <summary>
    /// Select the winners based on the sorted fitness scores
    /// </summary>
    /// <returns></returns>
    public List<GameObject> SelectWinners()
    {
        List<GameObject> selection = new List<GameObject>();
        for (int i = 0; i < _selectedChickenCount; i++)
            selection.Add(chickenList[i]);
        return selection;
    }


    /// <summary>
    /// Crossbreed chicken "mirroring" each other in the winners list
    /// Example : the 1st ranked chicken will get a child with the last ranked chicken
    /// The purpose is to balance the children's DNA
    /// </summary>
    /// <param name="winners"></param>
    public List<GameObject> CrossbreedWinners(List<GameObject> winners)
    {
        List<GameObject> nextGeneration = new();
        int lastIndex = winners.Count - 1;
        for (int i = 0; i <= winners.Count / 2 - 1; i++)
        {
            // Crossbreeding
            for(int j = 0; j < _childrenPerWinners; j++)
            {
                List<string> newDNA = winners[i].GetComponent<ChickenGenetic>().Crossbreed(winners[lastIndex - 1]);
                nextGeneration.Add(_generator.GenerateChicken(newDNA.Count, newDNA));
            }
        }
        return nextGeneration;
    }

    /// <summary>
    /// Remove all chicken from the list, for the next generation
    /// </summary>
    public void DeletePreviousGeneration()
    {
        for(int i = 0; i < chickenList.Count; i++)
        {
            Destroy(chickenList[i]);
        }
        chickenList.Clear();
    }

}
