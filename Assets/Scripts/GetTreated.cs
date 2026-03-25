using UnityEngine;
using UnityEngine.AI;

public class GetTreated : GAction
{
    public override bool PrePerform()
    {
        Debug.Log("GetTreated PrePerform START");

        target = agent.GetComponent<Doctor>().patient;

        if (target == null)
        {
            Debug.Log("GetTreated FALLA: no hay cubículo en inventario");
            return false;
        }

        Debug.Log("GetTreated OK: paciente encontrado " + target.name);
        return true;
    }

    public override bool PostPerform()
    {
        if (target != null)
        {
            GAgent patientAgent = target.GetComponent<GAgent>();

            // 1. Curamos al paciente
            patientAgent.beliefs.ModifyState("isCured", 1);

            // 2. IMPORTANTE: Le quitamos al paciente el estado de "esperando" 
            // para que su plan actual termine o cambie
            patientAgent.beliefs.ModifyState("waitingDoctor", -1);

            // Frenamos al doctor
            NavMeshAgent nav = agent.GetComponent<NavMeshAgent>();
            if (nav != null) { nav.isStopped = true; nav.ResetPath(); }

            // Liberamos el cubículo físico
            GameObject cubicleObj = inventory.FindItemWithTag("Cubicle");
            if (cubicleObj != null)
            {
                cubicleObj.GetComponent<Cubicle>().currentPatient = null;
                inventory.RemoveItem(cubicleObj);
            }

            // Restamos un paciente del contador global del mundo
            GWorld.Instance.GetWorld().ModifyState("patientInCubicle", -1);

            agent.GetComponent<Doctor>().patient = null;
        }

        // LIMPIEZA DE CREENCIAS (Beliefs)
        // Al eliminar 'atCubicle', el doctor ya no cumple la precondición de GetTreated
        // y se ve obligado a volver a GoToCubicleDoctor para el siguiente paciente.
        beliefs.ModifyState("atCubicle", -1);

        // Si tienes 'treatPatient' en el Inspector, el GOAP le sumará 1 automáticamente.
        // Al restarlo aquí, lo dejamos en 0 y el ciclo vuelve a empezar.
        beliefs.ModifyState("treatPatient", -1);

        target = null;
        return true;
    }

}
