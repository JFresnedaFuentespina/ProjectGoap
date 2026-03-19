using UnityEngine;

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
        GameObject patient = target;
        if (patient != null)
        {
            patient.GetComponent<GAgent>().beliefs.ModifyState("isCured", 1);
            Debug.Log("Paciente " + patient.name + " ha sido curado");
        }
        return true;
    }
}
