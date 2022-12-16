using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to create a single chicken but also a population. It's especially used for the 1st generation
/// </summary>
public class ChickenGenerator : MonoBehaviour
{
    [SerializeField] ChickenManager chickenManager;

    [Header("Chicken generation")]
    [SerializeField] private GameObject _chickenAsset; // Chicken's asset
    [SerializeField] private int _initialChickenAmount; // Initial amount of chicken

    [Header("Positions")]
    // Range for positions on the X axis
    [SerializeField] private float _minimumX;
    [SerializeField] private float _maximumX;
    // Position for the Y and Z axis
    [SerializeField] private float _positionY;
    [SerializeField] private float _positionZ; // Starting position

    [Header("Others")]
    [SerializeField] private int _maxDirectionsAmount; // Maximum amount of generated directions per time

    private void Start()
    {
    }

    /// <summary>
    /// Start of the simulation, with the 1st generation
    /// </summary>
    public void StartSimulation()
    {
        // Manager initialization
        chickenManager.currentChickenAmount = _initialChickenAmount;
        chickenManager.chickenList = new List<GameObject>();
        // Creation of the 1st generation
        GenerateFirstGeneration();
    }

    /*
     * Main function
     */

    /// <summary>
    /// Creation of the 1st generation
    /// </summary>
    private void GenerateFirstGeneration()
    {
        for (int i = 0; i < _initialChickenAmount; i++)
            chickenManager.chickenList.Add(GenerateChicken(i));
    }

    /*
     * Utilitary functions
     */

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chickenId">Id of the chicken inside its generation</param>
    /// <param name="DNA">The DNA representing its path. It's only empty for the first generation, since it's randomly generated</param>
    /// <returns></returns>
    public GameObject GenerateChicken(int chickenId, List<string> DNA = null, GameObject parentA = null, GameObject parentB = null)
    {
        // Establish the position
        Vector3 newPosition;
        if (parentA != null & parentB != null)
            newPosition = new Vector3(SetAveragePositionX(parentA, parentB), _positionY, _positionZ); // for the 2nd generation and next
        else
            newPosition = RandomizePosition(); // only for the first generation

        // Create the chicken
        GameObject chicken = Instantiate(_chickenAsset, newPosition, Quaternion.identity);
        chicken.name = NameChicken(chickenId);
        chicken.GetComponent<ChickenMove>().maxPathSize = _maxDirectionsAmount;
        // For the 1st generation
        if (DNA == null)
            chicken.GetComponent<ChickenMove>().SetupRandomDirections();
        // For the next generations
        else
            chicken.GetComponent<ChickenGenetic>().pathDNA = DNA;
        return chicken;
    }

    /// <summary>
    /// Give a random position to the chicken, at the start of the race. 
    /// The Y and Z axis positions are fixed, while the X axis position is randomly chosen.
    /// We make sure the chicken doesn't spawn outside of the ground.
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomizePosition()
    {
        return new Vector3(Random.Range(_minimumX, _maximumX), _positionY, _positionZ);
    }

    /// <summary>
    /// Setting the average position of a children, based on its parents
    /// </summary>
    /// <param name="partnerA">First parent</param>
    /// <param name="partnerB">Second parent</param>
    /// <returns></returns>
    private float SetAveragePositionX(GameObject partnerA, GameObject partnerB)
    {
        float partnerInitialPositionAX = partnerA.GetComponent<ChickenMove>().startingPosition.x;
        float partnerInitialPositionBX = partnerB.GetComponent<ChickenMove>().startingPosition.x;
        return (partnerInitialPositionBX - partnerInitialPositionAX) / 2 + partnerInitialPositionAX;
    }

    /// <summary>
    /// Give a name to the chicken
    /// The name is GXEY.
    /// X is the generation to which the chicken belongs 
    /// Y is the id of the chicken inside its generation
    /// </summary>
    /// <param name="chickenId">Id of the chicken inside its own generation</param>
    /// <returns></returns>
    private string NameChicken(int chickenId)
    {
        return "Chicken n°G" + chickenManager.generationNumber + "E"+ chickenId;
    }
}
