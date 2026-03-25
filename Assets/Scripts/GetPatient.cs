using UnityEngine;

public class GetPatient : GAction {

    // Resource in this case = cubicle
    GameObject resource;

    public override bool PrePerform() {

        // Set our target patient and remove them from the Queue
        target = GWorld.Instance.RemovePatient();
        // Check that we did indeed get a patient
        if (target == null)
            // No patient so return false
            return false;
        // Grab a free cubicle and remove it from the list
        resource = GWorld.Instance.RemoveCubicle();
        // Test did we get one?
        if (resource != null) {

            // Yes we have a cubicle
            inventory.AddItem(resource);
        } else {

            // No free cubicles so release the patient
            GWorld.Instance.AddPatient(target);
            target = null;
            return false;
        }

        //take away one cubicle being available from the world state
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        return true;
    }

    public override bool PostPerform() {

        // Remove a patient from the world
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target) {

            target.GetComponent<GAgent>().inventory.AddItem(resource);
            // Trigger the patient's GoToCubicle action by giving them the belief
            target.GetComponent<GAgent>().beliefs.ModifyState("atCubicle", 0); // 0 just adds the key
            
            // Add the patient to the nurse's inventory so she can "carry" them to the cubicle
            inventory.AddItem(target);
            Debug.Log("Nurse picked up patient: " + target.name + " with tag: " + target.tag);
        }
        return true;
    }
}
