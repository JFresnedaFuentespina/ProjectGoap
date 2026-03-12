using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Text states;

    void LateUpdate()
    {
        Dictionary<string, int> worldstates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach(KeyValuePair<string, int> s in worldstates)
        {
            states.text += s.Key + ", " + s.Value;
        }
    }
}
