using UnityEngine;

public class GoToCubicle : GAction
{

    public override bool PrePerform()
    {
        target = GWorld.Instance.RemovePatient();
        if (target == null)
        {
            return false;
        }

        return true;
    }

    public override bool PostPerform()
    {
        // 1. Recuperamos al paciente que la enfermera trae (asegúrate de tener el Tag "Patient")
        GameObject patient = inventory.FindItemWithTag("Patient");

        if (target != null && patient != null)
        {
            // 2. IMPORTANTE: Asignamos el paciente al cubículo para que el Doctor lo vea después
            target.GetComponent<Cubicle>().currentPatient = patient;

            Debug.Log($"[NURSE] Paciente {patient.name} dejado en {target.name}.");
            inventory.RemoveItem(patient);

            // FISICO: Soltamos al paciente
            patient.transform.SetParent(null);
            patient.transform.position = target.transform.position; // Lo dejamos en el cubículo
                
            UnityEngine.AI.NavMeshAgent patientNav = patient.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (patientNav != null) patientNav.enabled = true;
                
            patient.GetComponent<GAgent>().enabled = true;
        }

        // 3. Quitamos el cubículo del inventario (porque ya llegamos)
        inventory.RemoveItem(target);
        beliefs.ModifyState("patientPickedUp", -1);

        return true;
    }



}
