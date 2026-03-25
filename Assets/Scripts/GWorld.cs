using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Queue<GameObject> patients;
    private static Queue<GameObject> patientsWaitingDoctor;
    private static Queue<GameObject> cubicles;
    private static Dictionary<string, int> globalInts = new Dictionary<string, int>();
    private static Dictionary<int, bool> patientsBeingTreated = new Dictionary<int, bool>();
    private static int patientsGoneHome = 0;

    static GWorld()
    {
        world = new WorldStates();
        patients = new Queue<GameObject>();
        cubicles = new Queue<GameObject>();
        patientsWaitingDoctor = new Queue<GameObject>();

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (GameObject c in cubes)
        {
            cubicles.Enqueue(c);
        }

        if (cubes.Length > 0)
        {
            world.ModifyState("FreeCubicle", cubes.Length);
        }
        Time.timeScale = 4;
    }

    private GWorld()
    {

    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }

    public void AddPatientWaiting(GameObject p)
    {
        patientsWaitingDoctor.Enqueue(p);
        Debug.Log($"[GWORLD] Paciente añadido a la cola de espera. Total en cola: {patientsWaitingDoctor.Count}");
    }

    public GameObject RemovePatientWaiting()
    {
        if (patientsWaitingDoctor.Count == 0)
        {
            Debug.LogWarning("[GWORLD] Intento de sacar paciente, pero la cola está VACÍA.");
            return null;
        }
        GameObject p = patientsWaitingDoctor.Dequeue();
        Debug.Log($"[GWORLD] Paciente {p.name} sacado de la cola. Quedan: {patientsWaitingDoctor.Count}");
        return p;
    }

    public void AddPatient(GameObject p)
    {
        patients.Enqueue(p);
    }

    public GameObject RemovePatient()
    {
        if (patients.Count == 0) return null;
        return patients.Dequeue();
    }

    public void AddCubicle(GameObject c)
    {
        cubicles.Enqueue(c);
    }

    public GameObject RemoveCubicle()
    {
        if (cubicles.Count == 0) return null;
        return cubicles.Dequeue();
    }

    public void SetGlobal(string key, int value)
    {
        if (globalInts.ContainsKey(key))
            globalInts[key] = value;
        else
            globalInts.Add(key, value);
    }

    public int GetGlobal(string key)
    {
        if (globalInts.ContainsKey(key))
            return globalInts[key];
        return -1;
    }

    public bool HasGlobal(string key)
    {
        return globalInts.ContainsKey(key);
    }
    public void SetPatientBeingTreated(int patientID, bool value)
    {
        patientsBeingTreated[patientID] = value;
    }

    public bool IsPatientBeingTreated(int patientID)
    {
        return patientsBeingTreated.ContainsKey(patientID) && patientsBeingTreated[patientID];
    }
    public void PatientWentHome()
    {
        patientsGoneHome++;
    }

    public int GetPatientsGoneHome()
    {
        return patientsGoneHome;
    }
}
