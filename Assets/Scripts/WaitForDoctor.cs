using UnityEngine;

public class WaitForDoctor : GAction
{
    public override bool PrePerform()
    {
        int patientID = agent.gameObject.GetInstanceID();

        if (!beliefs.HasState("waitingDoctor"))
            return false;

        // Verifica si este paciente está siendo tratado
        if (!GWorld.Instance.IsPatientBeingTreated(patientID))
        {
            Debug.Log("Paciente " + agent.name + ": esperando a ser tratado por doctor...");
            return false;
        }

        return true;
    }
    public override bool PostPerform()
    {
        int patientID = agent.gameObject.GetInstanceID();
        GWorld.Instance.SetPatientBeingTreated(patientID, false); // liberar estado
        beliefs.ModifyState("isCured", 1);
        return true;
    }
}