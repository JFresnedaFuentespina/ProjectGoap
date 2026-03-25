using UnityEngine;

public class WaitForDoctor : GAction {
    public override bool PrePerform() {

        // The nurse added the cubicle to our inventory
        target = inventory.FindItemWithTag("Cubicle");
        if (target == null) return false;

        return true;
    }

    public override bool PostPerform() {

        // Inject waiting state to world states
        GWorld.Instance.GetWorld().ModifyState("WaitingForDoctor", 1);
        // Patient adds himself to the correct queue
        GWorld.Instance.AddPatientWaitingDoctor(this.gameObject);
        // Inject a state into the agents beliefs
        beliefs.ModifyState("waitingForDoctor", 1);
        Debug.Log("[PATIENT]: I am in the cubicle waiting for the doctor.");

        return true;
    }
}
