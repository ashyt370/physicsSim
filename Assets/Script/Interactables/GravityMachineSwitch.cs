using UnityEngine;

public class GravityMachineSwitch : Interactable
{
    public GameObject gravityMachine;

    private bool isOn = false;

    private void Start()
    {
        gravityMachine.GetComponent<BoxCollider>().enabled = false;
    }
    public override void Interact(GameObject player)
    {
        if(isOn)
        {
            gravityMachine.GetComponent<BoxCollider>().enabled = false;
            isOn = false;
        }
        else
        {
            gravityMachine.GetComponent<BoxCollider>().enabled = true;
            isOn = true;
        }
    }
}
