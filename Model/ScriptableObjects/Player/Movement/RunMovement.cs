using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovement", menuName = "PlayerMovement/Run", order = 51)]
public class RunMovement : PlayerMovement {

    private float inputJump;
    public FloatReference runSpeed;
    public float jumpForce;
    private bool isJumping;

    protected override void CheckInput()
    {
        inputV.Value = Input.GetAxis("Vertical");
        inputH.Value = Input.GetAxis("Horizontal");
        inputJump = Input.GetAxisRaw("Jump");
    }

    public override void Move()
    {
        CheckInput();

        if(activeRotate.Value)
            Rotate();

        // Si esta en el suelo
        if (collider.isGrounded)
        {
            direction.y = 0;
            if (activeMove.Value)
            {
                Run();
                if (inputJump > 0)
                    Jump();
            }
        }
        // Si esta en el aire (saltando o cayendo)
        else
        {
            Fall();
        }

        // Aplicar gravedad siempre y la direccion calculada
        direction.y += Physics.gravity.y * Time.deltaTime;
        collider.Move(direction * Time.deltaTime);  // SE MUEVE
    }

    private void Run()
    {
        playerAnimator.UpdateRun();
        // Si esta en modo combate, se mueve mirando al puntero, desplazamiento horizontal y vertical
        if (targetMode.Value)
        {
            direction.Set(inputH.Value, 0, inputV.Value);

            // Se ajusta la velocidad en diagonal
            if (direction.magnitude > 1)
            {
                direction /= direction.magnitude;
            }


            if (inputV.Value > 0)
            {
                direction *= runSpeed.Value;
            }
            else
            {
                direction *= runSpeed.Value / 1.5f;
            }
        }
        // Modo no combate, desplazamiento recto en el eje Z
        else
        {
            direction.Set(0, 0, Mathf.Max(Mathf.Abs(inputV.Value), Mathf.Abs(inputH.Value)) * runSpeed.Value);
        }

        // Transformamos la direccion de local a world space (relativa al transform del player)
        direction = playerTransform.TransformDirection(direction);
    }
    private void Fall()
    {
        if (direction.y < 0) // CAE
        {
            isJumping = false;
            playerEvents.OnFall.Invoke();
        }
        // Si choca la cabeza con el techo se frena el salto y comienza a caer
        else if ((collider.collisionFlags & CollisionFlags.Above) != 0)
        { 
            direction.y = 0;
        }
    }

    private void Jump()
    {
        isJumping = true;
        direction.y = jumpForce;
        playerEvents.OnJump.Invoke();
    }

    // Modifica la velocidad de movimiento
    private void SetSpeed(float speed)
    {
        runSpeed.Value = speed;
    }
}
