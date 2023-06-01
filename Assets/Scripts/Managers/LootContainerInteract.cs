using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject open;
    [SerializeField] bool opened;

    public override void Interact(Player player)
    {
        if(opened == false)
        {
            opened = true;
            closed.SetActive(false);
            open.SetActive(true);
        }
    }
}
