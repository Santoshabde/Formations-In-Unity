using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormationShape
{
    StraightLine,
    Circle,
    Square,
    Cone
}

public class Formation : MonoBehaviour
{
    [SerializeField] public FormationShape formationShape;
    public FormationShape FormationShapeValue => formationShape;

    [SerializeField] public GameObject indicator;
    [SerializeField] public FormationAgent formationAgent;
    //Circle Properties
    [SerializeField] public float radius;
    //Horizontal Line Properties
    [SerializeField] public float lenght;
    //Cone Properties
    public float angle;
    public float slantHeight;
    //Generic Properties
    [SerializeField] public int numberToSpawn;
    public int amountToSpawn;
    public int amountToRemove;
    public GameObject spawnEffect;
    public AudioSource onAddSound;

    //Private Variables Section
    private List<GameObject> indicatorsList = new List<GameObject>();
    private List<GameObject> agentsList = new List<GameObject>();
    private IShape currentFormationShape;

    private CircleFormation circleFormation;
    private HorizontalLineFormation horizontalLineFormation;
    private SquareFormation squareFormation;
    private ConeFormation coneFormation;

    private bool formShape;
    void Start()
    {
        //Circle
        circleFormation = new CircleFormation();
        circleFormation.radius = radius;

        //Horizontal Line formation
        horizontalLineFormation = new HorizontalLineFormation();
        horizontalLineFormation.length = lenght;

        //Square
        squareFormation = new SquareFormation();
        squareFormation.sideLength = lenght;

        //Cone
        coneFormation = new ConeFormation();
        coneFormation.angle = angle;

        //Current Shape
        switch (formationShape)
        {
            case FormationShape.StraightLine:
                currentFormationShape = horizontalLineFormation;
                break;
            case FormationShape.Circle:
                currentFormationShape = circleFormation;
                break;
            case FormationShape.Square:
                currentFormationShape = squareFormation;
                break;
            case FormationShape.Cone:
                currentFormationShape = coneFormation;
                break;
            default:
                break;
        }
        
        SpawnFormationPointsAndAgents(numberToSpawn);
    }

    void Update()
    {
        if (formShape)
        {
            currentFormationShape.Form(this.transform, indicatorsList);
            switch (formationShape)
            {
                case FormationShape.StraightLine:
                    currentFormationShape = horizontalLineFormation;
                    horizontalLineFormation.length = lenght;
                    break;
                case FormationShape.Circle:
                    currentFormationShape = circleFormation;
                    circleFormation.radius = radius;
                    break;
                case FormationShape.Square:
                    currentFormationShape = squareFormation;
                    squareFormation.sideLength = lenght;
                    break;
                case FormationShape.Cone:
                    currentFormationShape = coneFormation;
                    coneFormation.angle = angle;
                    coneFormation.slantHeight = slantHeight;
                    break;
                default:
                    break;
            }
        }
    }

    //Spawning formation agents and formation transforms - where agents are assigned each transform!!
    private void SpawnFormationPointsAndAgents(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject go = GameObject.Instantiate(indicator, Vector3.zero, Quaternion.identity);
            FormationAgent agent = Instantiate(formationAgent, transform.position , Quaternion.identity);
            agent.toFollow = go.transform;
            indicatorsList.Add(go);
            agentsList.Add(agent.gameObject);
        }

        formShape = true;
    }

    //Call this function if you want to add a formation agent into your existing formation!!
    public void AddFormationAgent(int count)
    {
        Instantiate(spawnEffect, transform.position, Quaternion.identity);
        onAddSound.Play();
        count = currentFormationShape.AmountToExpand(count);

        numberToSpawn += count;
        formShape = false;
        SpawnFormationPointsAndAgents(count);
    }

    public void RemoveFormationAgent(int count)
    {
        formShape = false;
        onAddSound.Play();
        count = currentFormationShape.AmountToExpand(count);

        for (int i = 0; i < count; i++)
        {
            onAddSound.Play();
            GameObject toRemove = indicatorsList[i];
            GameObject toRemoveAgent = agentsList[i];
            toRemove.SetActive(false);
            toRemoveAgent.SetActive(false);

            indicatorsList.RemoveAt(i);
            agentsList.RemoveAt(i);

            Instantiate(spawnEffect, toRemove.transform.position, Quaternion.identity);        
        }

        formShape = true;
    }
}
