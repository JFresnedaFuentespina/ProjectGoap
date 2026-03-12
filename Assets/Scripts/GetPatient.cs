using UnityEngine;

public class GetPatient : GAction
{

    public override bool PostPerform()
    {
        target = GWorld.Instance.RemovePatient(); // Quita al paciente de la cola
        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PrePerform()
    {
        return true;
    }
}
