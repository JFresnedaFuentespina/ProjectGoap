public class GoToHospital : GAction {
    public override bool PrePerform() {

        return true;
    }

    public override bool PostPerform() {

        // Patient is now at the hospital
        beliefs.ModifyState("hasArrived", 1);
        return true;
    }
}
