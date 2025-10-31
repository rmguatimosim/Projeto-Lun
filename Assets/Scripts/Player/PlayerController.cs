
using System;
using System.Collections.Generic;
using EventArgs;
using Player;
using Player.States;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    //Game manager instance
    private GameManager gm;
    

    //State machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpState;
    [HideInInspector] public ChangeForm changeFormState;
    [HideInInspector] public Assign assignState;
    [HideInInspector] public Hurt hurtState;
    [HideInInspector] public Dead deadState;
    [HideInInspector]public Cutscene cutsceneState;

    //Components
    [HideInInspector] public Rigidbody thisRigidBody;
    [HideInInspector] public Animator thisAnimator;
    [HideInInspector] public HealthScript thisHealth;


    //player controls
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction transformAction;
    private InputAction copyAction;
    private InputAction storeAction;
    public InputAction anyKeyAction;

    //Movement
    [Header("Movement")]
    [HideInInspector] public Vector2 movementVector;
    public float movementSpeed = 10;
    public float maxSpeed = 10;

    //Jump
    [Header("Jump")]
    public float jumpPower = 10;
    public float jumpMovementFactor = 1f;
    [HideInInspector] public bool hasJumpInput;
    [HideInInspector] public bool isGrounded;

    //Hurt
    [Header("Hurt")]
    public float hurtDuration = 0.2f;

    //Change form
    [Header("Change Form")]
    public float changeFormDuration = 0.5f;
    [HideInInspector] public bool hasChangeFormInput;
    [HideInInspector]public int formsIndex;
    public VarForm[] forms;
    public VarForm currentVarForm;
    public VarForm selectedVarForm;
    public List<GameObject> parts;
    private float cooldownToggleForm;
    private readonly float toggleFormTimer = 0.2f;
    private bool hasToggleForm;

    //assign tool
    [Header("Assigning tool")]
    public bool hasAssignTool;
    [HideInInspector] public bool hasCopyInput;
    [HideInInspector] public bool hasStoreInput;
    [HideInInspector] public int content;
    public event EventHandler<SaveContentArgs> OnCopy;
    [HideInInspector] public bool hasInteracted;



    [Header("Footsteps")]
    public AudioClip footstepSounds;
    public AudioSource footstepAudioSource;
    public float footstepInterval = 0.33f;

    //cutscene lock
    [HideInInspector] public bool isInCutscene;


    



    void Awake()
    {
        //get game manager instance
        gm = GameManager.Instance;

        //get components
        playerInput = GetComponent<PlayerInput>();
        thisRigidBody = GetComponent<Rigidbody>();
        thisAnimator = GetComponent<Animator>();
        thisHealth = GetComponent<HealthScript>();

        //subscribe to events
        thisHealth.OnDamage += OnDamage;
        thisHealth.OnHeal += OnHeal;

        //start playerInput
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");
        transformAction = playerInput.actions.FindAction("Transform");
        copyAction = playerInput.actions.FindAction("Copy");
        storeAction = playerInput.actions.FindAction("Store");
        anyKeyAction = playerInput.actions.FindAction("AnyKey");

    }



    // Start
    void Start()
    {
        //State machine and its states
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        changeFormState = new ChangeForm(this);
        assignState = new Assign(this);
        deadState = new Dead(this);
        hurtState = new Hurt(this);
        cutsceneState = new Cutscene(this);
        stateMachine.ChangeState(idleState);

        //hide mouse cursor while playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //select varForm
        formsIndex = 0;
        currentVarForm = forms[0];
        selectedVarForm = currentVarForm;
        gm.gameplayUI.SetTypeText();
        gm.gameplayUI.SetCurrentTypeText();
        gm.gameplayUI.SetValueText();

        //reset cooldown toggleForm;
        cooldownToggleForm = toggleFormTimer;


        //update UI
        var gameplayUI = GameManager.Instance.gameplayUI;
        gameplayUI.SetEnergyBar();

    }


    // Update is called once per frame
    void Update()
    {
        if (gm.isGameOver)
        {
            //show mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        //create input vector
        movementVector = moveAction.ReadValue<Vector2>();


        //receive jump input
        //hasJumpInput = jumpAction.ReadValue<float>() == 1;

        
        //has changeForm hold input
        transformAction.performed += context =>
        {
            if (context.interaction is HoldInteraction)
            {
                //if (!hasJumpInput && currentVarForm != selectedVarForm)
                if (!hasJumpInput)
                {
                    if (selectedVarForm.varType == VarType.Null && selectedVarForm == currentVarForm) return;
                    hasChangeFormInput = true;
                }
            }
            if (context.interaction is PressInteraction && hasToggleForm == false && !isInCutscene)
            {
                hasToggleForm = true;
                formsIndex++;
                if (formsIndex == forms.Length)
                {
                    formsIndex = 0;
                }
                selectedVarForm = forms[formsIndex];
                gm.gameplayUI.SetTypeText();
            }
        };
        transformAction.canceled += context =>
        {
            hasChangeFormInput = false;
        };

        //cooldown toggle changeForm
        if (hasToggleForm == true)
        {
            if ((cooldownToggleForm -= Time.deltaTime) <= 0)
            {
                hasToggleForm = false;
                cooldownToggleForm = toggleFormTimer;
            }
        }

        //check if has assign tool
        if (hasAssignTool)
        {
            //receive copy input
            if (copyAction.ReadValue<float>() == 1 && !hasJumpInput)
            {
                hasCopyInput = true;
            }

            //receive store input
            if (storeAction.ReadValue<float>() == 1 && !hasJumpInput)
            {
                hasStoreInput = true;
            }
        }


        //Update Animator
        float velocityRate = thisRigidBody.linearVelocity.magnitude / maxSpeed;
        thisAnimator.SetFloat("fVelocity", velocityRate);

        //Physics update
        DetectGround();

        //state machine update
        stateMachine.Update();

    }

    void LateUpdate()
    {
        stateMachine.LateUpdate();
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        LimitSpeed();
    }

    public Quaternion GetFoward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput(float alpha = 0.3f)
    {
        if (movementVector.IsZero()) return;
        //Calculate rotation
        Camera camera = Camera.main;
        Vector3 inputVector = new(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion toRotation = q1 * q2;
        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, alpha);

        //Apply rotation
        thisRigidBody.MoveRotation(newRotation);
    }

    private void DetectGround()
    {
        //Reset flag
        isGrounded = false;

        //Detect ground
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        float maxDistance = 0.1f;
        LayerMask groundLayer = GameManager.Instance.groundLayer;
        if (Physics.Raycast(origin, direction, maxDistance, groundLayer))
        {
            isGrounded = true;
        }
    }

    private void LimitSpeed()
    {
        Vector3 flatVelocity = new(thisRigidBody.linearVelocity.x, 0, thisRigidBody.linearVelocity.z);
        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;
            thisRigidBody.linearVelocity = new(limitedVelocity.x, thisRigidBody.linearVelocity.y, limitedVelocity.z);
        }
    }

    public void UpdateUI()
    {
        gm.gameplayUI.SetCurrentTypeText();
        gm.gameplayUI.SetValueText();
    }

    public void Copy(int receivedData, GameObject obj)
    {
        //invoke event
        OnCopy?.Invoke(this, new SaveContentArgs
        {
            content = receivedData,
            target = obj

        });
        UpdateUI();
    }

    private void OnDamage(object sender, DamageEventArgs args)
    {
        thisHealth.health -= args.damage;
        gm.gameplayUI.SetEnergyBar();
        gm.DecreaseScore();
        //switch to hurt
        stateMachine.ChangeState(hurtState);
    }
     private void OnHeal(object sender, HealEventArgs args)
    {
        thisHealth.health = thisHealth.maxHealth;
        gm.gameplayUI.SetEnergyBar();
    }



}


