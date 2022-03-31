using System;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject Heart;
    public GameObject FinishLine;

    public float Rate = 100f;
    public float FirstY = 2f;
    public float SecondY = -2f;
    private float Counter = 0f;
    private int PathCounter = 0;
    public int MaxPathCount = 5;

    void FixedUpdate()
    {
        if(GameManager.GameState != GameState.Game)
        {
            return;
        }

        if (PathCounter > MaxPathCount)
        {
            GenerateFinishLine();
            Destroy(gameObject);
        }

        if(Counter > Rate)
        {
            GeneratePath();
            Counter = 0f;
        }

        Counter++;
    }

    private void GenerateFinishLine()
    {
        Instantiate(FinishLine, new Vector3(transform.position.x, transform.position.y + FirstY, -3f), Quaternion.identity);
    }

    private void GeneratePath()
    {
        PathCounter++;

        if (PathCounter > MaxPathCount)
        {
            return;
        }

        Instantiate(Heart, new Vector3(transform.position.x, transform.position.y + FirstY, -3f), Quaternion.identity);
        Instantiate(Heart, new Vector3(transform.position.x, transform.position.y + SecondY, -3f), Quaternion.identity);
    }
}
