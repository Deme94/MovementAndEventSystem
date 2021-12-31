using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {

    private Animator anim;

    private int inputV_ID;
    private int inputH_ID;
    private int moveSpeedMultiplier_ID;

    public FloatReference inputV;
    public FloatReference inputH;
    public FloatReference runSpeed;
    public float normalizedRunSpeedAnimation;

    void Awake()
    {
        inputV_ID = Animator.StringToHash("InputV");
        inputH_ID = Animator.StringToHash("InputH");
        moveSpeedMultiplier_ID = Animator.StringToHash("RunSpeedMultiplier");
    }

    // Use this for initialization
    void Start () {
        // Componentes
        this.anim = GetComponent<Animator>();

        // Variables referenciadas
        inputV = GetComponent<PlayerMovementController>().inputV;
        inputH = GetComponent<PlayerMovementController>().inputH;
        runSpeed = GetComponent<PlayerMovementController>().run.runSpeed;

        // Eventos

        // Setup
        anim.SetFloat(moveSpeedMultiplier_ID, 
            (Mathf.Max(Mathf.Abs(inputV.Value), Mathf.Abs(inputH.Value)) * runSpeed.Value / normalizedRunSpeedAnimation));
    }

    public void UpdateRun()
    {
        anim.SetFloat(inputV_ID, inputV.Value);
        anim.SetFloat(inputH_ID, inputH.Value);
        anim.SetFloat(moveSpeedMultiplier_ID, (Mathf.Max(Mathf.Abs(inputV.Value), Mathf.Abs(inputH.Value)) * runSpeed.Value / normalizedRunSpeedAnimation));
    }
}
