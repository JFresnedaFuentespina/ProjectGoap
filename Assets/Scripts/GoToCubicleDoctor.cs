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

        // Verifica que haya al menos un paciente en algún cubículo
        if (!GWorld.Instance.GetWorld().HasState("patientInCubicle"))
        {
            Debug.Log("Doctor PrePerform: No hay paciente en cubículo aún");
            return false;
        }

        // Verifica que GWorld tenga asignado un cubículo
        if (!GWorld.Instance.HasGlobal("PatientCubicleID"))
        {
            Debug.Log("Doctor PrePerform: No hay ID de cubículo asignado en GWorld");
            return false;
        }

        int cubicleID = GWorld.Instance.GetGlobal("PatientCubicleID");
        Debug.Log("Doctor PrePerform: Buscando cubículo con ID = " + cubicleID);

        // Buscar cubículo correcto
        Cubicle[] allCubicles = GameObject.FindObjectsOfType<Cubicle>();
        foreach (var c in allCubicles)
        {
            Debug.Log("Doctor PrePerform: Revisando cubículo " + c.name + " con ID " + c.id);
            if (c.id == cubicleID)
            {
                target = c.gameObject;

                // Verifica que haya un paciente en el cubículo
                if (c.currentPatient != null)
                {
                    int patientID = c.currentPatient.GetInstanceID();
                    GWorld.Instance.SetPatientBeingTreated(patientID, true);
                    Debug.Log("Doctor PrePerform: Paciente " + c.currentPatient.name + " será tratado");
                }
                else
                {
                    Debug.Log("Doctor PrePerform: Cubículo encontrado pero sin paciente asignado");
                    return false;
                }

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
        Cubicle cubicleComp = target.GetComponent<Cubicle>();
        if (cubicleComp != null && cubicleComp.currentPatient != null)
        {
            Doctor doctor = agent.GetComponent<Doctor>();
            doctor.patient = cubicleComp.currentPatient;

            int patientID = cubicleComp.currentPatient.GetInstanceID();
            GWorld.Instance.SetPatientBeingTreated(patientID, true);

            // Una vez que el paciente está asignado, elimina el cubículo de la lista global
            GWorld.Instance.RemoveGlobal("PatientCubicleID");

            // Agregar cubículo al inventario si quieres
            inventory.AddItem(target);

            // Activar la meta para GOAP
            beliefs.ModifyState("treatPatient", 1);

            Debug.Log("Doctor GoToCubicleDoctor: paciente " + cubicleComp.currentPatient.name + " está siendo tratado y cubículo agregado al inventario");
        }

        return true;
    }
}