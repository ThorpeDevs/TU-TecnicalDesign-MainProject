using System;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class DefaultCarry : Interaction
{
    [Header("Primary Object References")]
    [Tooltip("Make Sure This Is The Same As The Value Above")][SerializeField]private CarryInteractType carryInteractType;
    [SerializeField]private GameObject playerArmature;
    
    [Header("Primary Values")]
    [Tooltip("Distance From Players Head")][SerializeField]private float playerDistance = 3f;

    private Rigidbody selfRigidBody;
    private void OnEnable()
    {
        selfRigidBody = GetComponent<Rigidbody>();
        
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
            gameObject.transform.SetParent(null);
            selfRigidBody.useGravity = true;
        }
        else
        {
            selfRigidBody.useGravity = false;
            gameObject.transform.position = new Vector3(playerArmature.transform.position.x, playerArmature.transform.position.y + playerDistance, playerArmature.transform.position.z);
            gameObject.transform.rotation = playerArmature.transform.rotation;
        
            gameObject.transform.SetParent(playerArmature.transform);
            Debug.Log(playerArmature.transform.position);
        }
    }

    private void UseLong(GameObject instigator, InteractionPhase phase, float delta)
    {
        switch (phase)
        {
            case InteractionPhase.CONTINUED:
                selfRigidBody.useGravity = false;
                gameObject.transform.position = new Vector3(playerArmature.transform.position.x, playerArmature.transform.position.y + playerDistance, playerArmature.transform.position.z);
                gameObject.transform.rotation = playerArmature.transform.rotation;
        
                gameObject.transform.SetParent(playerArmature.transform);
                Debug.Log(playerArmature.transform.position);
                break;
            
            case InteractionPhase.CANCELLED:
                gameObject.transform.SetParent(null);
                selfRigidBody.useGravity = true;
                break;
        }
    }
}

public enum CarryInteractType
{Short, Long}
