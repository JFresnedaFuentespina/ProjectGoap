using UnityEngine;

public class GoHome : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("isHome", 1);
        GWorld.Instance.PatientWentHome();
        int goneHome = GWorld.Instance.GetPatientsGoneHome();
        Debug.Log("Pacientes que se han ido: " + goneHome);

        if (goneHome > 0 && goneHome % 3 == 0)
        {
            // Marca al doctor como exhausto
            GameObject doctor = GameObject.FindWithTag("Doctor");
            if (doctor != null)
            {
                doctor.GetComponent<Doctor>().SetExhausted();
                doctor.GetComponent<GAgent>().Replan();
            }
        }

        Destroy(gameObject, 2f);
        return true;
    }

}
