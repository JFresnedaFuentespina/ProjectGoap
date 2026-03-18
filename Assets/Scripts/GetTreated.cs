using UnityEngine;

public class GetTreated : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null) return false;
        return true;
    }

    public override bool PostPerform()
    {
        GameObject patient = target.GetComponent<Cubicle>().currentPatient;
        if (patient != null)
        {
            patient.GetComponent<GAgent>().beliefs.ModifyState("isCured", 1);
            Debug.Log("Paciente " + patient.name + " ha sido curado");
        }
        return true;
    }
}
