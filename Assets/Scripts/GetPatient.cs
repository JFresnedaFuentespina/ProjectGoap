using UnityEngine;

public class GetPatient : GAction
{
    GameObject resource;

    public override bool PrePerform()
    {
        target = GWorld.Instance.RemovePatient(); // Quita al paciente de la cola
        if (target == null)
        {
            return false;
        }
        resource = GWorld.Instance.RemoveCubicle();
        if (resource != null)
        {
            inventory.AddItem(resource);
        }
        else
        {
            GWorld.Instance.AddPatient(target);
            target = null;
            return false;
        }

        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1); // le quitamos un cubiculo libre
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target)
        {
            target.GetComponent<GAgent>().inventory.AddItem(resource);
        }
        return true;
    }
}
