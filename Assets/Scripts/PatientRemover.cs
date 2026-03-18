using UnityEngine;

public class PatientRemover : MonoBehaviour
{
    public PatientManager pm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Patient"))
        {
            pm.count--;
        }
    }
}
