using UnityEngine;
public class TreatPatient : GAction {
    public override bool PrePerform() {

        return true;
    }

    public override bool PostPerform() {

        // The doctor has finished treating the patient
        GWorld.Instance.GetWorld().ModifyState("treatPatient", 1);
        
        // Cleanup inventories
        // target is the cubicle
        inventory.RemoveItem(target);

        // Find the patient in the doctor's inventory and remove them
        GameObject patient = inventory.FindItemWithTag("Patient");
        if (patient == null) {
            foreach (GameObject item in inventory.items) {
                if (item != null && item.GetComponent<Patient>() != null) {
                    patient = item;
                    break;
                }
            }
        }
        if (patient != null) inventory.RemoveItem(patient);

        return true;
    }
}
