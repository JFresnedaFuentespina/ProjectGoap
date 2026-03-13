using UnityEngine;

public class Patient : GAgent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        Subgoal s1 = new Subgoal("isWaiting", 1, true);
        goals.Add(s1, 3);

        Subgoal s2 = new Subgoal("isTreated", 1, true);
        goals.Add(s2, 5);
    }
}
