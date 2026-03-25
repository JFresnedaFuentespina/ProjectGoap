public class Register : GAction {
    public override bool PrePerform() {

        return true;
    }

    public override bool PostPerform() {

        // Patient is now registered
        beliefs.ModifyState("hasRegistered", 1);
        return true;
    }
}
