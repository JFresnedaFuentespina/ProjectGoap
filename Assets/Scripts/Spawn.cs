using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject patientPrefab;
    public PatientManager pm;
    public bool canSpawn = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void Update()
    {
        if (CanSpawn())
            SpawnPatient();
    }

    bool CanSpawn()
    {
        return (pm.count < pm.maxPatients);
    }

    void SpawnPatient()
    {
        pm.count++;
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
    }
}
