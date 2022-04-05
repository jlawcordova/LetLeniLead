using UnityEngine;

[DefaultExecutionOrder(2)]
public class PathGenerator : MonoBehaviour
{
    public GameObject Heart;
    public GameObject Heart2x;
    public GameObject FinishLine;

    public float Rate = 100f;

    public float[] SplitY = new float[] { -4.5f, 0f };
    public float[] BulkY = new float[] { -3.5f, -2.67f, -1.84f, -1f };
    private float Counter = 0f;
    private int PathCounter = 0;
    public int MaxPathCount = 5;

    void Start()
    {
        MaxPathCount = 4 + Mathf.Clamp(LevelManager.Instance.Level / 3, 1, 6);
        Rate = 100f - Mathf.Clamp((LevelManager.Instance.Level * 4), 1, 60);
    }

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

        var chance = Random.Range(0, 100);

        if (chance > 55)
        {
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
        else if (chance > 20)
        {
            var pattern = Random.Range(0, 2);
            
            switch (pattern)
            {
                case 0:
                    GenerateUp();
                    break;
                case 1:
                    GenerateDown();
                    break;
                default:
                    break;
            }
        }
        else if (chance > 11)
        {
            var bulkY = BulkY[Random.Range(0, BulkY.Length)];

            InstantiateRandomHeart(transform.position.x, transform.position.y + bulkY);
        }
        else if(chance > 4)
        {
            var bulkY = BulkY[Random.Range(0, BulkY.Length)];

            GenerateBulk(bulkY);
        }
        else 
        {
            GenerateSpecial();
        }
    }

    private void GenerateSpecial()
    {
        InstantiateRandomHeart(transform.position.x - 5f, transform.position.y - 1f, false, true);
        InstantiateRandomHeart(transform.position.x - 4f, transform.position.y - 3.5f + 0.83f + 0.83f, false, true);
        InstantiateRandomHeart(transform.position.x - 3f, transform.position.y - 3.5f + 0.83f, false, true);
        InstantiateRandomHeart(transform.position.x - 2f, transform.position.y - 3.5f, false, true);
        InstantiateRandomHeart(transform.position.x - 1f, transform.position.y - 3.5f - 0.83f, false, true);

        InstantiateRandomHeart(transform.position.x, transform.position.y - 3.5f, false, true);
        InstantiateRandomHeart(transform.position.x + 1f, transform.position.y - 3.5f + 0.83f, false, true);
        InstantiateRandomHeart(transform.position.x + 2f, transform.position.y - 3.5f + 0.83f + 0.83f, false, true);
        InstantiateRandomHeart(transform.position.x + 3f, transform.position.y - 1f, true);
    }

    private void GenerateBulk(float bulkY)
    {
        InstantiateRandomHeart(transform.position.x, transform.position.y + bulkY);
        InstantiateRandomHeart(transform.position.x, transform.position.y + bulkY + 0.8f);
        InstantiateRandomHeart(transform.position.x + 0.8f, transform.position.y + bulkY + 0.8f);
        InstantiateRandomHeart(transform.position.x + 0.8f, transform.position.y + bulkY);
    }

    private void GenerateDown()
    {
        InstantiateRandomHeart(transform.position.x + 2f, transform.position.y - 3.5f);
        InstantiateRandomHeart(transform.position.x + 1f, transform.position.y - 3.5f + 0.83f);
        InstantiateRandomHeart(transform.position.x, transform.position.y - 3.5f + 0.83f + 0.83f);
        InstantiateRandomHeart(transform.position.x -1f, transform.position.y - 1f);
    }

    private void GenerateUp()
    {
        InstantiateRandomHeart(transform.position.x - 1, transform.position.y - 3.5f);
        InstantiateRandomHeart(transform.position.x, transform.position.y - 3.5f + 0.83f);
        InstantiateRandomHeart(transform.position.x + 1f, transform.position.y - 3.5f + 0.83f + 0.83f);
        InstantiateRandomHeart(transform.position.x + 2f, transform.position.y - 1f);
    }

    private void GenerateTriple(float y)
    {
        InstantiateRandomHeart(transform.position.x, y);
        InstantiateRandomHeart(transform.position.x - 1, y);
        InstantiateRandomHeart(transform.position.x + 1f, y);
    }

    private void GenerateDouble(float y)
    {
        InstantiateRandomHeart(transform.position.x - 0.5f, y);
        InstantiateRandomHeart(transform.position.x + 0.5f, y);
    }

    private void GenerateSingle(float y)
    {
        InstantiateRandomHeart(transform.position.x, y);
    }

    private void InstantiateRandomHeart(float x, float y, bool sure2x = false, bool sure1 = false)
    {
        var chance = Random.Range(0f, 100f);
        var levelChanceBonus = Mathf.Clamp(LevelManager.Instance.Level *.65f, 0f, 7f);
        if ((chance > (95 - levelChanceBonus) || sure2x) && !sure1)
        {
            Instantiate(Heart2x, new Vector3(x, y + 0.36f, -3f), Quaternion.identity);
        }
        else
        {
            Instantiate(Heart, new Vector3(x, y, -3f), Quaternion.identity);
        }
    }
}

public enum Pattern
{
    Single,
    Double,
    Triple,
}
