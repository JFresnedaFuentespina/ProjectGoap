using UnityEngine;

public class Doctor : GAgent
{
    new void Start()
    {
        base.Start();

        // Meta principal: tratar pacientes
        Subgoal s1 = new Subgoal("treatPatient", 1, false);
        goals.Add(s1, 5);

        // Meta de descanso
        Subgoal s2 = new Subgoal("rested", 1, true);
        goals.Add(s2, 2);
    }

    // Llamado desde GoHome cuando corresponde
    public void SetExhausted()
    {
        beliefs.ModifyState("exhausted", 1);
        Debug.Log("Doctor está exhausto y puede descansar");
    }
}