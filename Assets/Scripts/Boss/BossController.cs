
using Behaviors.MeleeCreature.States;
using UnityEngine;
using UnityEngine.AI;

public class MeleeCreatureController : MonoBehaviour
{
    [HideInInspector] public MeleeCreatureHelper helper;
    [HideInInspector] public NavMeshAgent thisAgent;
    [HideInInspector] public Animator thisAnimator;
    [HideInInspector] public Collider thisCollider;
    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Transform avatar;

    //State Machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Follow followState;
    [HideInInspector] public Attack attackState;
    [HideInInspector] public Hurt hurtState;
    [HideInInspector] public Dead deadState;


    [Header("General")]
    public float searchRadius = 5f;
    private int bossHealth = 0;
    //[HideInInspector]
    public bool defeated;

    [Header("Idle")]
    public float targetSearchInterval = 1f;

    [Header("Follow")]
    public float ceaseFollowInterval = 4f;

    [Header("Attack")]
    public float distanceToAttack = 1f;
    public float attackSpeed = 10f;
    public float chargeDuration = 1f;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool hitWall;

    [Header("Hurt")]
    public float hurtDuration = 1f;

    [Header("Dead")]
    public float destroyIfFar = 30f;

    [Header("Effects")]
    public GameObject knockoutEffect;


    void Awake()
    {
        // start helper
        this.helper = new MeleeCreatureHelper(this);

        //Get components
        thisAgent = GetComponent<NavMeshAgent>();
        thisAnimator = GetComponentInChildren<Animator>();
        thisCollider = GetComponent<Collider>();
        thisRigidbody = GetComponent<Rigidbody>();
        avatar = GetComponentInChildren<Transform>();
    }

    void Start()
    {
        //Create StateMachine
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        followState = new Follow(this);
        attackState = new Attack(this);
        hurtState = new Hurt(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        //update stateMachine
        stateMachine.Update();

        if (defeated)
        {
            return;
        }

        if (bossHealth == 3)
        {
            defeated = true;
        }

        //update animator
        var velocityRate = thisAgent.velocity.magnitude / thisAgent.speed;
        thisAnimator.SetFloat("fVelocity", velocityRate);
    }

    void LateUpdate()
    {
        //update stateMachine
        stateMachine.LateUpdate();
    }
    void FixedUpdate()
    {
        //update stateMachine
        stateMachine.FixedUpdate();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isAttacking)
        {
            if (other.CompareTag("Wall") || other.CompareTag("Pylon"))
            {
                isAttacking = false;
                stateMachine.ChangeState(hurtState);
                var pylon = other.GetComponent<PylonScript>();
                if (pylon != null)
                {
                    if (pylon.wasActivated && pylon.canDamageBoss)
                    {
                        Debug.Log("Boss tomou dano");
                        bossHealth++;
                        pylon.canDamageBoss = false;
                        pylon.SetTextBroken();
                    }
                }
                else
                {
                    hitWall = true;
                }
            }

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            GameManager.Instance.player.GetComponent<HealthScript>().Damage(2);
        }
        if (collision.gameObject.CompareTag("Fragment"))
        {
            Destroy(collision.gameObject);
        }
        
    }



}
