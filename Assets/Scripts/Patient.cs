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
        
        // Subgoal s3 = new Subgoal("isCured", 1, true);
        // goals.Add(s3, 5);

        Subgoal s4 = new Subgoal("isHome", 1, true);
        goals.Add(s4, 5);
    }
}
