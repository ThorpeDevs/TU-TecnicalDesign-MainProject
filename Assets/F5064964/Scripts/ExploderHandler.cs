using UnityEngine;
using UnityEngine.Rendering;

public class ExploderHandler : Interaction
{
    private void Start()
    {
        OnShortInteraction += Testing;
    }
    
    private void Testing(GameObject instigator)
    {
        Debug.Log("Test");
    }
}
