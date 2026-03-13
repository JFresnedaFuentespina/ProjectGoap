using UnityEngine;

public class GoToWaitingRoom : GAction
{

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", 1); // Cuando llega, activa este flag en el mundo
        GWorld.Instance.AddPatient(gameObject); // Añade el paciente a la cola
        beliefs.ModifyState("atHospital", 1);
        return true;
    }
}
