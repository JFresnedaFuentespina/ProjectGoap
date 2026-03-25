using UnityEngine;
public class GoToCubicle : GAction
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

        // Add a new state "patientReady"
        GWorld.Instance.GetWorld().ModifyState("patientReady", 1);

        // Find the patient in the nurse's inventory
        GameObject patient = inventory.FindItemWithTag("Patient");
        
        // Fallback: search for any object with a Patient component if tag fails
        if (patient == null) {
            foreach (GameObject item in inventory.items) {
                if (item != null && item.GetComponent<Patient>() != null) {
                    patient = item;
                    break;
                }
            }
        }

        if (patient != null)
        {
            // Remove the patient from the nurse's inventory
            inventory.RemoveItem(patient);
        }
        else
        {
            string invItems = "";
            foreach (GameObject item in inventory.items) invItems += item.name + " (" + item.tag + "), ";
            Debug.LogWarning("Patient not found in inventory! Items in inventory: " + invItems);
        }

        // We do NOT release the cubicle here anymore.
        // The patient/doctor will do it after treatment.
        inventory.RemoveItem(target);
        return true;
    }
}
