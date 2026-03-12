using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numPatients;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numPatients; i++)
        {
            SpawnPatient();
        }

        Invoke("SpawnPatient", 5);
    }

    void SpawnPatient()
    {
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
        Invoke("SpawnPatient", Random.Range(2, 10));
    }
}
