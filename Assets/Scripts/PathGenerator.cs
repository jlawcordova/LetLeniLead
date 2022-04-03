using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject Heart;
    public GameObject FinishLine;

    public float Rate = 100f;

    public float[] SplitY = new float[] { -4.5f, 0f };
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
        Instantiate(FinishLine, new Vector3(transform.position.x, -2.25f, -3f), Quaternion.identity);
    }

    private void GeneratePath()
    {
        PathCounter++;

        if (PathCounter > MaxPathCount)
        {
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            var pattern = (Pattern)Random.Range(0, 3);

            switch (pattern)
            {
                case Pattern.Single:
                    GenerateSingle(transform.position.y + SplitY[i]);
                    break;
                case Pattern.Double:
                    GenerateDouble(transform.position.y + SplitY[i]);
                    break;
                case Pattern.Triple:
                    GenerateTriple(transform.position.y + SplitY[i]);
                    break;
                default:
                    break;
            }
        }
        
    }

    private void GenerateTriple(float y)
    {
        Instantiate(Heart, new Vector3(transform.position.x, y, -3f), Quaternion.identity);
        Instantiate(Heart, new Vector3(transform.position.x - 1f, y, -3f), Quaternion.identity);
        Instantiate(Heart, new Vector3(transform.position.x + 1f, y, -3f), Quaternion.identity);
    }

    private void GenerateDouble(float y)
    {
        Instantiate(Heart, new Vector3(transform.position.x - 0.5f, y, -3f), Quaternion.identity);
        Instantiate(Heart, new Vector3(transform.position.x + 0.5f, y, -3f), Quaternion.identity);
    }

    private void GenerateSingle(float y)
    {
        Instantiate(Heart, new Vector3(transform.position.x, y, -3f), Quaternion.identity);
    }
}

public enum Pattern
{
    Single,
    Double,
    Triple,
}
