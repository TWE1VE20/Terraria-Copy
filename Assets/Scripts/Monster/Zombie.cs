using System.Collections;
using UnityEditor;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamageable
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Monster monsterStat;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer ZombieSprite;
    [SerializeField] GroundChecker groundChecker;
    [SerializeField] Gore gorePrefab;

    [Header("Property")]
    [SerializeField] float movePower;
    [SerializeField] float breakPower;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxXSpeed;
    [SerializeField] float jumpSpeed;

    [Header("Ground Check")]
    [SerializeField] public LayerMask groundLayer;


    private StateMachine fsm;
    private Transform playerTransform;

    public bool isGround => groundChecker.IsGround;

    public int hp { get; private set; }
    public bool isGrounded { get; private set; }


    private void Start()
    {
        hp = monsterStat.Hp;
        if(monsterStat.id == 3)
        {
            // animator¿« layer ¡∂¡§
        }

        fsm = new StateMachine();
        fsm.Init(this);

        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        fsm.Update();
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<IDamageable>().TakeHit(5, gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<IDamageable>().TakeHit(5, gameObject);
        }
    }

    public void TakeHit(int damage, GameObject attacker)
    {
        hp -= damage;
        Vector3 knockbackDirection = (transform.position - attacker.transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * monsterStat.knockBack, ForceMode2D.Impulse);
    }

    #region State
    public enum State { Idle, Trace, Return, Die, Size }
    private class StateMachine
    {
        private State curState;
        private BaseState[] states;

        public void Init(Zombie owner)
        {
            states = new BaseState[(int)State.Size];

            states[(int)State.Idle] = new IdleState(this, owner);
            states[(int)State.Trace] = new TraceState(this, owner);
            states[(int)State.Return] = new ReturnState(this, owner);
            states[(int)State.Die] = new DieState(this, owner);

            curState = State.Idle;
            states[(int)curState].Enter();
        }

        public void Update()
        {
            states[(int)curState].Update();
            states[(int)curState].Transition();
        }

        public void ChangeState(State nextState)
        {
            states[(int)curState].Exit();
            curState = nextState;
            states[(int)curState].Enter();
        }
    }

    private class BaseState
    {
        protected StateMachine fsm;
        protected Zombie owner;

        public BaseState(StateMachine fsm, Zombie owner)
        {
            this.fsm = fsm;
            this.owner = owner;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Transition() { }
        public virtual void Exit() { }
    }

    private class IdleState : BaseState
    {
        public IdleState(StateMachine fsm, Zombie owner) : base(fsm, owner)
        {
        }

        public override void Transition()
        {
            if (owner.hp <= 0)
            {
                fsm.ChangeState(State.Die);
            }
            fsm.ChangeState(State.Trace);
        }
    }

    private class TraceState : BaseState
    {
        public TraceState(StateMachine fsm, Zombie owner) : base(fsm, owner) { }

        public override void Update()
        {
            Vector2 dir = (owner.playerTransform.position - owner.transform.position).normalized;
            if (dir.x < 0 && owner.rigid.velocity.x > -owner.maxXSpeed)
            {
                owner.ZombieSprite.flipX = false;
                owner.rigid.AddForce(Vector2.right * dir.x * owner.movePower);
                owner.animator.SetBool("Move", true);
            }
            else if (dir.x > 0 && owner.rigid.velocity.x < owner.maxXSpeed)
            {
                owner.ZombieSprite.flipX = true;
                owner.rigid.AddForce(Vector2.right * dir.x * owner.movePower);
                owner.animator.SetBool("Move", true);
            }
            else
            {
                owner.animator.SetBool("Move", false);
            }

            if (owner.rigid.velocity.x < 0.01f && owner.rigid.velocity.x > -0.01f && dir.x != 0)
            {
                if (owner.isGrounded)
                {
                    Debug.Log("Jump");
                    Vector2 velocity = owner.rigid.velocity;
                    velocity.y = owner.jumpSpeed;
                    owner.rigid.velocity = velocity;
                }
            }
        }

        public override void Transition()
        {
            if (owner.hp <= 0)
            {
                fsm.ChangeState(State.Die);
            }
        }
    }

    private class ReturnState : BaseState
    {
        public ReturnState(StateMachine fsm, Zombie owner) : base(fsm, owner)
        {
        }

        public override void Transition()
        {
            if (owner.hp <= 0)
            {
                fsm.ChangeState(State.Die);
            }
        }
    }

    private class DieState : BaseState
    {
        private Coroutine routine;

        public DieState(StateMachine fsm, Zombie owner) : base(fsm, owner){}

        public override void Enter()
        {
            // routine = owner.StartCoroutine(DieRoutine());
            if(owner.monsterStat.gore.Count !=0)
            {
                Gore gore = Instantiate(owner.gorePrefab, owner.transform.position, owner.transform.rotation);
                // gore.GetComponent<SpriteRenderer> = null;
                // dropeditem.numberOf = numberofItem + num - 999;
                // dropeditem.invenManager = mainSlot;
            }
            Destroy(owner.gameObject);
        }

        /*
        IEnumerator DieRoutine()
        {
            yield return new WaitForSeconds(1);
            Destroy(owner.gameObject);
        }
        */
    }

    #endregion
}
