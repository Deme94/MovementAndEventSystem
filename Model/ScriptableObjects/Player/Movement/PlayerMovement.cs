using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : ScriptableObject {

    public FloatReference inputV;
    public FloatReference inputH;
    public BoolReference targetMode;
    public BoolReference activeRotate;
    public BoolReference activeMove;

    protected Transform playerTransform;
    protected CharacterController collider;
    protected PlayerAnimatorController playerAnimator;
    protected PlayerEventsManager playerEvents;
    protected Transform playerLookAt;
    protected Vector3 direction;

    public virtual void Setup(Transform player, Transform lookAt, CharacterController controller, Vector3 moveDirection,
        PlayerAnimatorController animator, PlayerEventsManager events)
    {
        playerTransform = player;
        playerLookAt = lookAt;
        collider = controller;
        direction = moveDirection;
        playerAnimator = animator;
        playerEvents = events;
    }

    protected abstract void CheckInput();
    public abstract void Move();
    protected void Rotate()
    {
        if (targetMode.Value)
        {
            playerTransform.rotation = Quaternion.Euler(0, playerLookAt.eulerAngles.y, 0);
        }
        else
        {
            if (inputV.Value != 0 || inputH.Value != 0)
            {
                playerTransform.rotation =
                Quaternion.Euler(0, playerLookAt.eulerAngles.y + Mathf.Atan2(inputH.Value, inputV.Value) * 180 / Mathf.PI, 0);
            }
        }
    }
}
