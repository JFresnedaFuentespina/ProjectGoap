using UnityEngine;
public class GoToCubicleDoctor : GAction
{

    public override bool PrePerform()
    {
        // NEW: Check if the nurse has arrived at the cubicle first
        if (GWorld.Instance.GetWorld().GetStates().ContainsKey("patientReady") == false) {
            return false; // Not ready yet, stay at current position
        }

        // Dequeue the patient from the waiting for doctor queue
        target = GWorld.Instance.RemovePatientWaitingDoctor();
        // Check that we did indeed get a patient
        if (target == null)
            // No patient so return false
            return false;

        // The nurse added the cubicle to the patient's inventory
        // We need to find it to know where to go
        GameObject cubicle = target.GetComponent<GAgent>().inventory.FindItemWithTag("Cubicle");
        if (cubicle == null) {

            // No cubicle found in patient inventory, put patient back in queue
            GWorld.Instance.AddPatientWaitingDoctor(target);
            target = null;
            return false;
        }

        // Add the patient to the doctor's inventory so the doctor knows who they are treating
        inventory.AddItem(target);
        // Add the cubicle to the doctor's inventory so it can be found in the next action
        inventory.AddItem(cubicle);

        // Set the doctor's target as the cubicle where the patient is
        target = cubicle;
        // All good
        return true;
    }

    public override bool PostPerform()
    {

        // The doctor is now at the cubicle with the patient
        GWorld.Instance.GetWorld().ModifyState("readyToTreat", 1);
        return true;
    }
}
