using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEngine;
using TMPro;

/// <summary>
/// Manage everything related to the chicken. It's specifically used to switch between generations.
/// </summary>
public class ChickenManager : MonoBehaviour
{
    [HideInInspector] public int generationNumber = 1;
    [HideInInspector] public int currentChickenAmount; // Number of remaining chicken running
    [HideInInspector] public List<GameObject> chickenList; // List of all generated chicken
    private ChickenGenerator _generator;

    [Header("Winners parameters")]
    [SerializeField] private int _winnerBonus = 2;
    [SerializeField] private int _selectedChickenCount; // Number of chicken selected for breeding
    [SerializeField, Range(1, 4)] private int _childrenPerWinners = 2;

    private void Start()
    {
        _generator = gameObject.GetComponent<ChickenGenerator>();
    }

    /// <summary>
    /// Setting up the new generation
    /// </summary>
    public void SetupNextGeneration()
    {
        generationNumber++;
        CalculateAllScores();
        SortByFitnessScores();
        List<GameObject> nextGeneration = CrossbreedWinners(SelectWinners());
        DeletePreviousGeneration();
        chickenList = nextGeneration;
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
    /// Sort the chicken by their fitness score, from hightest to lowest
    /// </summary>
    public void SortByFitnessScores()
    {
        // Sort in ascending order
        chickenList.Sort(delegate (GameObject chickenA, GameObject chickenB)
        {
            return (chickenA.GetComponent<ChickenGenetic>()._fitnessScore).CompareTo(chickenB.GetComponent<ChickenGenetic>()._fitnessScore);
            //return (int)(chickenA.GetComponent<ChickenGenetic>()._fitnessScore - chickenB.GetComponent<ChickenGenetic>()._fitnessScore);
        });
        // Then reverse the list
        chickenList.Reverse();
    }

    /// <summary>
    /// Select the winners based on the sorted fitness scores
    /// </summary>
    /// <returns></returns>
    public List<GameObject> SelectWinners()
    {
        List<GameObject> selection = new List<GameObject>();
        for (int i = 0; i < _selectedChickenCount; i++)
        {
            Debug.Log("Fitness score n° " + i + " = " + chickenList[i].gameObject.GetComponent<ChickenGenetic>()._fitnessScore);
            selection.Add(chickenList[i]);
        }
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
        //Debug.Log("Number of winners :" + winners.Count);
        List<GameObject> nextGeneration = new();
        int lastIndex = winners.Count - 1;
        int id = 0;
        //Debug.Log("First half of winners ends with chicken n°" + (winners.Count / 2 - 1));
        for (int i = 0; i <= winners.Count / 2 - 1; i++)
        {
            //Debug.Log("Couple n°" + i);
            //Debug.Log("Partner id is " + (lastIndex - 1 - i));
            // Crossbreeding
            for(int j = 0; j < _childrenPerWinners; j++)
            {
                //Debug.Log("Creating child n° " + j);
                List<string> newDNA = winners[i].GetComponent<ChickenGenetic>().Crossbreed(winners[lastIndex - 1 - i]);
                nextGeneration.Add(_generator.GenerateChicken(id, newDNA, winners[i], winners[lastIndex - 1 - i]));
                id++;
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
