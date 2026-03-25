using UnityEngine;

public class GoToCubicleNurse : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null)
        {
            Debug.Log("Nurse PrePerform: No hay cubículo en el inventario");
            return false;
        }
        Debug.Log("Nurse PrePerform: Cubículo encontrado = " + target.name);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("patientInCubicle", 1);

        // Guardamos el ID del cubículo asignado
        Cubicle cubicleComp = target.GetComponent<Cubicle>();
        if (cubicleComp != null)
        {
            GWorld.Instance.SetGlobal("PatientCubicleID", cubicleComp.id);
            Debug.Log("Nurse PostPerform: Asignado cubículo con ID = " + cubicleComp.id + " en GWorld");

            // **Asignar paciente al cubículo**
            Nurse nurse = agent.GetComponent<Nurse>();
            GameObject patient = nurse.carryingPatient;
            if (patient != null)
            {
                cubicleComp.currentPatient = patient;
                Debug.Log("Nurse PostPerform: paciente " + patient.name + " asignado al cubículo");

                // FISICO: Soltamos al paciente
                patient.transform.SetParent(null);
                patient.transform.position = target.transform.position; // Lo dejamos en el cubículo
                
                UnityEngine.AI.NavMeshAgent patientNav = patient.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (patientNav != null) patientNav.enabled = true;
                
                patient.GetComponent<GAgent>().enabled = true;
            }
            else
            {
                Debug.Log("Nurse PostPerform: ERROR, no se encontró el paciente en el inventario");
            }
        }

        GWorld.Instance.AddCubicle(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        beliefs.RemoveState("patientPickedUp");

        Debug.Log("Nurse PostPerform: patientInCubicle marcado en el mundo");

        return true;
    }
}