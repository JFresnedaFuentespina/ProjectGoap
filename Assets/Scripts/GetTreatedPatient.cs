using UnityEngine;

public class GetTreatedPatient : GAction
{
    public bool isDoctor = false;

    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null) return false;

        return true;
    }

    public override bool PostPerform()
    {
        // marca que el paciente ahora está esperando al doctor
        beliefs.ModifyState("waitingDoctor", 1);
        Debug.Log("Paciente GetTreated: esperando al doctor");
        return true;
    }
}
