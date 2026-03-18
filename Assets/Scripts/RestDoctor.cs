using UnityEngine;

public class RestDoctor : GAction
{
    public override bool PrePerform()
    {
        // Solo planifica si doctor está exhausto
        if (beliefs.HasState("exhausted"))
        {
            Debug.Log("Doctor necesita descansar");
            return true;
        }
        return false;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("rested", 1);
        beliefs.RemoveState("exhausted");
        Debug.Log("Doctor ha descansado");
        return true;
    }
}