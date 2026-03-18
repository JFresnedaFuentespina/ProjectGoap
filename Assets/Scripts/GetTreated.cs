using UnityEngine;

public class GetTreated : GAction
{
    public bool isDoctor = false;

    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null) return false;

        // if (!beliefs.HasState("beingTreated")) return false;

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Treated", 1);
        beliefs.ModifyState("isCured", 1);

        GWorld.Instance.AddCubicle(target);
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        inventory.RemoveItem(target);

        return true;
    }
}
