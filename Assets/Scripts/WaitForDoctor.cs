using UnityEngine;

public class WaitForDoctor : GAction
{
    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        // 1. Notifica al mundo que hay alguien listo
        GWorld.Instance.GetWorld().ModifyState("waitingForDoctor", 1);

        // 2. Se añade a sí mismo a la cola
        GWorld.Instance.AddPatientWaiting(this.gameObject);

        // 3. Se marca como "en el cubículo" para sus propias metas
        beliefs.ModifyState("atCubicle", 1);

        Debug.Log($"[PATIENT] {gameObject.name} registrado en la cola de espera del Doctor.");
        return true;
    }

}