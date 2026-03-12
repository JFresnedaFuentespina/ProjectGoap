using UnityEngine;

public class Nurse : GAgent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        Subgoal s1 = new Subgoal("treatPatient", 1, true);
        goals.Add(s1, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
