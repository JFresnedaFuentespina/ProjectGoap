using Unity;

public class GetTreated : GAction
{

    public override bool PrePerform()
    {

        // Get a free cubicle
        target = inventory.FindItemWithTag("Cubicle");
        // Check that we did indeed get a cubicle
        if (target == null)
            // No cubicle so return false
            return false;
        // All good
        return true;
    }

    public override bool PostPerform()
    {

        // Add a new state "isCured" (previously Treated)
        GWorld.Instance.GetWorld().ModifyState("isCured", 1);
        // Add isCured to agents beliefs
        beliefs.ModifyState("isCured", 1);
        
        // Give back the cubicle to the world
        GWorld.Instance.AddCubicle(target);
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", 1);
        
        // Remove the cubicle from the patient's inventory
        inventory.RemoveItem(target);
        return true;
    }
}
