using UnityEngine;

public class WaitForDoctor : GAction
{
    public override bool PrePerform()
    {
        if (!beliefs.HasState("waitingDoctor"))
            return false;

        // Aquí el paciente mira si el doctor ya empezó a tratarlo
        if (!GWorld.Instance.GetWorld().HasState("treatPatient"))
        {
            Debug.Log("Paciente WaitForDoctor: esperando al doctor...");
            return false;
        }

        return true;
    }
    public override bool PostPerform()
    {
        beliefs.ModifyState("isCured", 1); // paciente recibe el tratamiento
        Debug.Log("Paciente WaitForDoctor: tratamiento completado, puede irse.");
        return true;
    }
}