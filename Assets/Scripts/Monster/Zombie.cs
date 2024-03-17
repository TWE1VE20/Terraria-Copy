using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Monster monsterStat;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer ZombieSprite;

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

    private Collider2D groundCollider;  // 바닥 체크 콜라이더

    public int hp { get; private set; }
    public bool isGrounded { get; private set; }


    private void Start()
    {
        hp = monsterStat.Hp;
        if(monsterStat.id == 3)
        {
            // animator의 layer 조정
        }

        fsm = new StateMachine();
        fsm.Init(this);

        groundCollider = GetComponent<Collider2D>();

        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        fsm.Update();
    }

    private void FixedUpdate()
    {
        isGrounded = CheckGround();
    }

    public void TakeHit(int damage)
    {
        hp -= damage;
    }

    bool CheckGround()
    {
        Vector2 rayOrigin = groundCollider.bounds.center;
        Vector2 rayDirection = Vector2.down;
        float rayDistance = groundCollider.bounds.extents.y;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayer);

        if (hit.collider != null)
            return true;
        else
            return false;
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
            else if ((owner.rigid.velocity.x < 0.03f && owner.rigid.velocity.x > -0.03f) && dir.x != 0)
            {
                Debug.Log("Try Jump");
                if (owner.isGrounded) 
                {
                    Debug.Log("Jump");
                    Vector2 velocity = owner.rigid.velocity;
                    velocity.y = owner.jumpSpeed;
                    owner.rigid.velocity = velocity;
                }
            }
            else
            {
                owner.animator.SetBool("Move", false);
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

        public DieState(StateMachine fsm, Zombie owner) : base(fsm, owner)
        {
        }

        public override void Enter()
        {
            routine = owner.StartCoroutine(DieRoutine());
        }

        IEnumerator DieRoutine()
        {
            yield return new WaitForSeconds(1);
            Destroy(owner.gameObject);
        }

    }

    #endregion
}
