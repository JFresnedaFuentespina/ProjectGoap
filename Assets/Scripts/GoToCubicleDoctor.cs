using UnityEngine;

public class GoToCubicleDoctor : GAction
{
    void Start()
    {
        targetTag = "Cubicle"; // Debe coincidir con el tag del cubículo en la escena
    }

    public override bool PrePerform()
    {
        // 1. Intentamos sacar al siguiente paciente de la cola
        target = GWorld.Instance.RemovePatientWaiting();

        if (target == null) return false;

        // 2. El objetivo físico del Doctor es el Cubículo donde está el paciente
        GameObject patient = target;
        Cubicle[] allCubicles = GameObject.FindObjectsByType<Cubicle>(FindObjectsSortMode.None);
        
        target = null; // Reiniciamos target para buscar el cubículo
        
        foreach (var c in allCubicles)
        {
            if (c.currentPatient == patient)
            {
                // Guardamos al paciente en la variable del Doctor
                agent.GetComponent<Doctor>().patient = patient;
                // El objetivo físico del Doctor es el Cubículo
                target = c.gameObject;
                break;
            }
        }

        return target != null;
    }

    public override bool PostPerform()
    {
        // Notificamos al mundo que ya no está esperando
        GWorld.Instance.GetWorld().ModifyState("waitingForDoctor", -1);

        // Le damos el estado para que pueda pasar a la acción GetTreated
        beliefs.ModifyState("atCubicle", 1);
        return true;
    }

}