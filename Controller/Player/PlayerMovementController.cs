using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public Transform lookAt;

    PlayerEventsManager events;
    
    private Vector3 direction;                      // Vector de la dirección

    public BoolReference activeMove;                // Si es false el jugador no se movera
    public BoolReference activeRotate;
    public BoolReference targetMode;                // Si es true rotara siempre adonde mire la camara

    public FloatReference inputV;                   // Tecla de avance recto
    public FloatReference inputH;                   // Tecla de avance lateral

    public RunMovement run;

    private PlayerMovement activeMovement;

    void Awake()
    {
        direction = Vector3.zero;
        activeMove.Value = true;
        activeRotate.Value = true;
        targetMode.Value = false;
    }

    // Use this for initialization
    void Start()
    {
        CharacterController controller;
        PlayerAnimatorController animatorController;
        controller = GetComponent<CharacterController>();
        animatorController = GetComponent<PlayerAnimatorController>();
        events = GetComponent<PlayerEventsManager>();

        events.OnTargetMode.AddListener(SwitchTargetMode);

        run.Setup(transform, lookAt, controller, direction, animatorController, events);
        activeMovement = run;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            events.OnTargetMode.Invoke();
        }
        activeMovement.Move();
    }

    private void SwitchTargetMode()
    {
        targetMode.Value = !targetMode.Value;
    }
}
