using UnityEngine;

public class VolunterGenerator : MonoBehaviour
{
    public float Rate = 25f;
    private float Counter = 25f;
    public GameObject[] Volunteers;
    public float StartX = -10f;
    public float MinY = -4.5f;
    public float MaxY = 4.5f;


    void FixedUpdate()
    {
        if(Counter > Rate)
        {
            if (ScoreManager.ScoreValue > 0)
            {
                GenerateVolunteer();
            }
            Counter = 0f;
        }

        Counter++;
    }

    private void GenerateVolunteer()
    {
        var volunteerIndex = Random.Range(0, Volunteers.Length);

        var volunteerGameObject = Volunteers[volunteerIndex];
        var volunteer = volunteerGameObject.GetComponent<Volunteers>();

        Instantiate(volunteerGameObject,
            new Vector3(
                StartX,
                Random.Range(MinY, MaxY),
                -5f),
            Quaternion.identity);
        ScoreManager.DeductScore(volunteer.ScoreValue);
    }
}
