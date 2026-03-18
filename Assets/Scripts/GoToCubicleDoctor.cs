using UnityEngine;

public class GoToCubicleDoctor : GAction
{
    void Start()
    {
        targetTag = "Cubicle"; // Debe coincidir con el tag del cubículo en la escena
    }

    public override bool PrePerform()
    {
        Debug.Log("Doctor PrePerform: Verificando precondiciones...");

        if (!GWorld.Instance.GetWorld().HasState("patientInCubicle"))
        {
            Debug.Log("Doctor PrePerform: No hay paciente en cubículo aún");
            return false;
        }

        if (!GWorld.Instance.HasGlobal("PatientCubicleID"))
        {
            Debug.Log("Doctor PrePerform: No hay ID de cubículo asignado en GWorld");
            return false;
        }

        int cubicleID = GWorld.Instance.GetGlobal("PatientCubicleID");
        Debug.Log("Doctor PrePerform: Buscando cubículo con ID = " + cubicleID);

        Cubicle[] allCubicles = GameObject.FindObjectsOfType<Cubicle>();
        foreach (var c in allCubicles)
        {
            Debug.Log("Doctor PrePerform: Revisando cubículo " + c.name + " con ID " + c.id);
            if (c.id == cubicleID)
            {
                target = c.gameObject;
                break;
            }
        }

        if (target == null)
        {
            Debug.Log("Doctor PrePerform: No se encontró el cubículo con ID = " + cubicleID);
        }
        else
        {
            Debug.Log("Doctor PrePerform: Cubículo asignado como target = " + target.name);
        }

        return target != null;
    }

    public override bool PostPerform()
    {
        // El doctor marca que está tratando al paciente
        GWorld.Instance.GetWorld().ModifyState("treatPatient", 1);
        Debug.Log("Doctor GoToCubicleDoctor: paciente está siendo tratado");
        return true;
    }
}