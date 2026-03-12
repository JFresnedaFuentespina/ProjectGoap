using UnityEngine;

public class GoToWaitingRoom : GAction
{

    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", 1); // Cuando llega, activa este flag en el mundo
        GWorld.Instance.AddPatient(this.gameObject); // Añade el paciente a la cola
        return true;
    }
}
