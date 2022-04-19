using UnityEngine;

public class VolunterGenerator : MonoBehaviour
{
    public int IncreaseRate = 5;
    public int IncreaseCounter = 0;
    public float Rate = 25f;
    private float Counter = 25f;
    public GameObject[] Volunteers;
    public float StartX = -10f;
    public float MinY = -4.5f;
    public float MaxY = 4.5f;
    public AudioClip Sound;


    void FixedUpdate()
    {
        if (IncreaseCounter > IncreaseRate)
        {
            Rate = Mathf.Clamp(Rate - 5f, 5f, 50f);
            IncreaseCounter = 0;
        }

        if(Counter > Rate)
        {
            if (ScoreManager.ScoreValue > 0)
            {
                IncreaseCounter++;
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
        var y = volunteer.Bottom ? MinY + 0.5f : Random.Range(MinY, MaxY);
        var z = Mathf.Clamp(volunteerGameObject.transform.position.z + (y * 0.5f), -9.5f, 0);

        Instantiate(volunteerGameObject,
            new Vector3(
                StartX,
                y,
                z),
            Quaternion.identity);
            AudioManager.Play("Volunteer", Sound, 1, false);
        ScoreManager.DeductScore(volunteer.ScoreValue);
    }
}
