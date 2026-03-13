using UnityEngine;

public class Nurse : GAgent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        Subgoal s1 = new Subgoal("treatPatient", 1, false);
        goals.Add(s1, 3);

        Subgoal s2 = new Subgoal("rested", 1, false);
        goals.Add(s2, 1);

        Invoke("GetTired", Random.Range(10, 20));
    }

    void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        Invoke("GetTired", Random.Range(10,20));   
    }
}
