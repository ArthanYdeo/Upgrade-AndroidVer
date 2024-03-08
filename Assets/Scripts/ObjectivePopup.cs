using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePopup : MonoBehaviour
{
       public GameObject objectivePanel; // Assign your objective panel UI here

    // This method is called when dialogue finishes
    public void ShowObjectivePopup()
    {
        objectivePanel.SetActive(true); // Activate the objective panel
    }
}
