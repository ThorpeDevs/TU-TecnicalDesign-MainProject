using System;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DefaultCarry : Interaction
{
    [Header("Primary Object References")]

    [Tooltip("Make Sure This Is The Same As The Value Above")]
    [SerializeField]private CarryInteractType carryInteractType;
    [SerializeField]private GameObject playerArmature;
    

    [Header("Distance Values")]

    [Tooltip("Distance From Players Head")]
    [SerializeField]private float playerDistance = 3f;

    [Tooltip("Adjust Placement Position (If Not Using Throw)")]
    [SerializeField] private Vector3 placementPosition = new Vector3 (0, 1, 0);

    [Header("Throw Values")]
    [Tooltip("Does The Player Throw The Item When Dropped?")]
    [SerializeField] private bool doThrow = false;

    [Tooltip("Force Item Is Thrown At (time * 100)")]
    [SerializeField] private float throwForce = 5;

    [Header("Unity Event")]
    [SerializeField] public UnityEvent OnThrowEvent;
    private Rigidbody selfRigidBody;

    private void OnEnable()
    {
        selfRigidBody = GetComponent<Rigidbody>();
        throwForce = throwForce * 100;

        switch (carryInteractType)
        {
            case CarryInteractType.Short:
            OnShortInteraction += UseShort;
            break;
            
            case CarryInteractType.Long:
            OnLongInteraction += UseLong;
            break;
        }
    }

    private void UseShort(GameObject instigator)
    {
        if (gameObject.transform.parent != null)
        {
            DropItem();
        }
        else
        {
            CarryItem();
        }
    }

    private void UseLong(GameObject instigator, InteractionPhase phase, float delta)
    {
        switch (phase)
        {
            case InteractionPhase.CONTINUED:

                CarryItem();
                break;
            
            case InteractionPhase.CANCELLED:

                DropItem();
                break;
        }
    }

    private void CarryItem()
    {
        selfRigidBody.useGravity = false;
        gameObject.transform.position = new Vector3(playerArmature.transform.position.x, playerArmature.transform.position.y + playerDistance, playerArmature.transform.position.z);
        gameObject.transform.rotation = playerArmature.transform.rotation;

        gameObject.transform.SetParent(playerArmature.transform);
    }

    private void DropItem()
    {
        gameObject.transform.SetParent(null);
        selfRigidBody.useGravity = true;

        if (doThrow)
        {
            selfRigidBody.AddForce(playerArmature.transform.forward * 500);
        }
        else
        {
            gameObject.transform.position = (playerArmature.transform.position + playerArmature.transform.forward + placementPosition);
        }

        if (OnThrowEvent != null)
        {
            OnThrowEvent.Invoke();
        }
    }
}

public enum CarryInteractType
{Short, Long}
