using UnityEngine;

[DefaultExecutionOrder(2)]
public class PathGenerator : MonoBehaviour
{
    public GameObject Heart;
    public GameObject HeartPlus2;
    public GameObject HeartPlus3;
    public GameObject Heart3x;
    public GameObject[] Energy;
    public GameObject Rosas;
    public GameObject PowerUp;
    public GameObject FinishLine;

    public float Rate = 100f;

    public float[] SplitY = new float[] { -4.5f, 0f };
    public float[] BulkY = new float[] { -3.5f, -2.67f, -1.84f, -1f };
    private float Counter = 0f;
    private int PathCounter = 0;
    public int MaxPathCount = 5;

    #region Energy

    public int EnergyPityTimer = 700;
    public int EnergyPityTimerCounter = 0;

    #endregion

    #region PowerUp

    public int PowerUpTimer = 1500;
    public int PowerUpTimerCounter = 0;

    #endregion

    void Start()
    {
        MaxPathCount = 5 + Mathf.Clamp(LevelManager.Instance.Level, 1, 5);
        Rate = 100f - Mathf.Clamp((LevelManager.Instance.Level * 4), 1, 60);
    }

    void FixedUpdate()
    {
        if(GameManager.GameState != GameState.Game)
        {
            return;
        }

        if (GameManager.Instance.Frozen)
        {
            return;
        }

        // Never end.
        // if (PathCounter > MaxPathCount)
        // {
        //     GenerateFinishLine();
        //     Destroy(gameObject);
        // }

        if(Counter > Rate)
        {
            GeneratePath();
            Counter = 0f;
        }

        Counter++;

        HandleEnergyPityTimer();
        HandlePowerUpTimer();
    }

    private void HandleEnergyPityTimer()
    {
        EnergyPityTimerCounter++;

        if (EnergyPityTimerCounter >= EnergyPityTimer)
        {
            EnergyPityTimerCounter = 0;
            InstantiateEnergy(transform.position.x, -2.5f, -4f, true);
        }
    }

    private void HandlePowerUpTimer()
    {
        PowerUpTimerCounter++;
    }

    private void GenerateFinishLine()
    {
        Instantiate(FinishLine, new Vector3(transform.position.x, -2.25f, -3f), Quaternion.identity);
    }

    private void GeneratePath()
    {
        var chance = Random.Range(0, 100);

        if (chance > 60)
        {
            GenerateSplit();
        }
        else if (chance > 40)
        {
            if (PowerUpTimerCounter > PowerUpTimer)
            {
                InstantiatePowerUp(transform.position.x);
            }
            else
            {
                GenerateSplit();
            }
        }
        else if (chance > 25)
        {
            GenerateLine();
        }
        else if (chance > 15)
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
        else if (chance > 4)
        {
            var bulkY = BulkY[Random.Range(0, BulkY.Length)];

            GenerateBulk(bulkY);
        }
        else if(chance > 2)
        {
            GenerateSpecial();
        }
        else 
        {
            var bulkY = BulkY[Random.Range(0, BulkY.Length)];
            InstantiateEnergy(transform.position.x, transform.position.y + bulkY, -4f);
        }
    }

    private void GenerateSplit()
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

    private void InstantiatePowerUp(float x)
    {
        var y = BulkY[Random.Range(0, BulkY.Length)];
        Instantiate(PowerUp, new Vector3(x, y, -3f + (0.1f * (y))), Quaternion.identity);
        PowerUpTimerCounter = 0;
    }

    private void GenerateLine()
    {
        InstantiateRandomHeart(transform.position.x - 0.5f, transform.position.y - 3.5f - 0.83f);
        InstantiateRandomHeart(transform.position.x - 0.5f, transform.position.y - 3.5f);
        InstantiateRandomHeart(transform.position.x - 0.5f, transform.position.y - 3.5f + 0.83f);
        InstantiateRandomHeart(transform.position.x - 0.5f, transform.position.y - 3.5f + 0.83f + 0.83f);
        InstantiateRandomHeart(transform.position.x - 0.5f, transform.position.y - 1f);

        InstantiateRandomHeart(transform.position.x + 0.5f, transform.position.y - 3.5f - 0.83f);
        InstantiateRandomHeart(transform.position.x + 0.5f, transform.position.y - 3.5f);
        InstantiateRandomHeart(transform.position.x + 0.5f, transform.position.y - 3.5f + 0.83f);
        InstantiateRandomHeart(transform.position.x + 0.5f, transform.position.y - 3.5f + 0.83f + 0.83f);
        InstantiateRandomHeart(transform.position.x + 0.5f, transform.position.y - 1f);
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

        if (!GameManager.Instance.Times3Unlocked)
        {
            InstantiateRandomHeart(transform.position.x + 3f, transform.position.y - 1f, false, true);
        }
        else
        {
            InstantiateRandomHeart(transform.position.x + 3f, transform.position.y - 1f, true);
        }
    }

    private void GenerateBulk(float bulkY)
    {
        InstantiateRandomHeart(transform.position.x + 0.8f, transform.position.y + bulkY + 0.8f);
        InstantiateRandomHeart(transform.position.x + 0.8f, transform.position.y + bulkY);
        InstantiateRandomHeart(transform.position.x, transform.position.y + bulkY);
        InstantiateRandomHeart(transform.position.x, transform.position.y + bulkY + 0.8f);
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

    private void InstantiateEnergy(float x, float y, float z, bool sureBouncing = false,
        bool sureStatic = true)
    {
        // Check if the energy should be bouncing.
        // If it is, use the first energy game object which bounces.
        var energyIndex = sureBouncing ? 0 :
            sureStatic ? 1 :
            Random.Range(0, Energy.Length);

        Instantiate(Energy[energyIndex], new Vector3(x, y, z), Quaternion.identity);
    }

    private void InstantiateRandomHeart(float x, float y, bool sure2x = false, bool sure1 = false)
    {
        var chance = Random.Range(0f, 150f);
        var levelChanceBonus = Mathf.Clamp(LevelManager.Instance.Level * 1f, 1f, 20f);

        if (sure2x)
        {
            Instantiate(Heart3x, new Vector3(x, y + 0.36f, -3f + (0.1f * (y + 0.36f))), Quaternion.identity);
            return;
        }

        
        if (chance > 100 || sure1)
        {
            Instantiate(Heart, new Vector3(x, y, -3f + (0.1f * (y))), Quaternion.identity);
            return;
        }
        else if ((chance > (90 - levelChanceBonus)) && !sure1)
        {
            Instantiate(HeartPlus2, new Vector3(x, y + 0.36f, -3f + (0.1f * (y + 0.36f))), Quaternion.identity);
            return;
        }
        else if ((chance > (80 - levelChanceBonus)) && !sure1)
        {
            Instantiate(HeartPlus3, new Vector3(x, y + 0.36f, -3f + (0.1f * (y + 0.36f))), Quaternion.identity);
            return;
        }
        else if (chance > 5)
        {
            Instantiate(Heart, new Vector3(x, y, -3f + (0.1f * (y))), Quaternion.identity);
            return;
        }
        else if (chance > 2)
        {
            Instantiate(Rosas, new Vector3(x, y, -3f + (0.1f * (y))), Quaternion.identity);
            return;
        }
        else if (chance > 1){
            if (!GameManager.Instance.Times3Unlocked)
            {
                Instantiate(Heart, new Vector3(x, y, -3f + (0.1f * (y))), Quaternion.identity);
            }
            else
            {
                Instantiate(Heart3x, new Vector3(x, y + 0.36f, -3f + (0.1f * (y + 0.36f))), Quaternion.identity);
            }
            return;
        }
        else
        {
            InstantiateEnergy(x, y, -4f, false, true);
        }
    }
}

public enum Pattern
{
    Single,
    Double,
    Triple,
}
