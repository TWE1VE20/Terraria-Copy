using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] int maxHp;
    [SerializeField] int hp;

    [Header("Layermask")]
    public Tilemap blockMap;
    public Tilemap foreGroundmap;
    public Tilemap wallMap;
    [SerializeField] LayerMask mineLayer;
    [SerializeField] LayerMask cutDownLayer;
    [SerializeField] LayerMask WallCrushLayer;

    [Header("Prefab")]
    [SerializeField] SpriteRenderer holdItem;
    [SerializeField] DropItem dropItemPrefab;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private StateMachine fsm;

    private int currentSlot;
    private Item currentItem;

    private void Start()
    {
        currentSlot = 0;
        currentItem = null;

        fsm = new StateMachine();
        fsm.Init(this);
    }

    private void Update()
    {
        if (inventoryManager.current != currentSlot || inventoryManager.mainInventory[currentSlot].item != currentItem)
        {
            this.currentSlot = inventoryManager.current;
            this.currentItem = inventoryManager.mainInventory[currentSlot].item;
            this.holdItem.sprite = currentItem.image;
        }
        fsm.Action();
    }

    public void flipPlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = (ray.origin - transform.position).normalized;
        gameObject.GetComponent<PlayerController>().Flip(direction.x);
    }

    #region State
    public enum State { Alive, Dead, Size }
    private class StateMachine
    {
        private State curState;
        private BaseState[] states;
        public void Init(PlayerAction player)
        {
            states = new BaseState[(int)State.Size];
            states[(int)State.Alive] = new AliveState(this, player);
            states[(int)State.Dead] = new AliveState(this, player);

            curState = State.Alive;
            states[(int)curState].Enter();
        }

        public void Action()
        {
            states[(int)curState].Action();
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
        protected PlayerAction player;
        protected Item item => player.currentItem;
        public BaseState(StateMachine fsm, PlayerAction player)
        {
            this.fsm = fsm;
            this.player = player;
        }

        public virtual void Enter() { }
        public virtual void Action() { }
        public virtual void Exit() { }
    }

    private class AliveState : BaseState
    {
        public AliveState(StateMachine fsm, PlayerAction player) : base(fsm, player) { }
        public override void Action()
        {
            if (player.hp <= 0)
            {
                fsm.ChangeState(State.Dead);
            }
            else
            {
                switch (item.type)
                {
                    case ItemType.None:
                        break;
                    case ItemType.Equipment:
                        break;
                    case ItemType.Tool:
                        switch (item.actionType)
                        {
                            case ActionType.None:
                                break;
                            case ActionType.Mine:
                                if (Input.GetMouseButtonDown(0))
                                {
                                    // 마우스 커서 위치에서 타일 좌표 가져오기
                                    Vector3 mousePosition = Input.mousePosition;
                                    Vector3Int tilePosition = player.blockMap.WorldToCell(Camera.main.ScreenToWorldPoint(mousePosition));

                                    // 타일 설정
                                    TileBase tile = player.blockMap.GetTile(tilePosition);
                                    if (tile != null)
                                    {
                                        AdvancedRuleTile tileData = tile as AdvancedRuleTile;
                                        DropItem dropeditem = Instantiate(player.dropItemPrefab, tilePosition, player.transform.rotation);
                                        dropeditem.item = tileData.item;
                                        dropeditem.numberOf = 1;
                                        dropeditem.invenManager = player.inventoryManager;
                                        player.blockMap.SetTile(tilePosition, null);
                                    }
                                    player.animator.SetTrigger("Swing");
                                }
                                break;
                            case ActionType.WallCrush:
                                if (Input.GetMouseButtonDown(0))
                                {
                                    // 마우스 커서 위치에서 타일 좌표 가져오기
                                    Vector3 mousePosition = Input.mousePosition;
                                    Vector3Int tilePosition = player.wallMap.WorldToCell(Camera.main.ScreenToWorldPoint(mousePosition));

                                    // 타일 설정
                                    TileBase tile = player.wallMap.GetTile(tilePosition);
                                    if (tile != null)
                                    {
                                        AdvancedRuleTile tileData = tile as AdvancedRuleTile;
                                        DropItem dropeditem = Instantiate(player.dropItemPrefab, tilePosition, player.transform.rotation);
                                        dropeditem.item = tileData.item;
                                        dropeditem.numberOf = 1;
                                        dropeditem.invenManager = player.inventoryManager;
                                        player.wallMap.SetTile(tilePosition, null);
                                    }
                                    player.animator.SetTrigger("Swing");
                                }
                                break;
                        }
                        break;
                    case ItemType.Weapon:
                        if (Input.GetMouseButtonDown(0))
                        {
                            player.flipPlayer();
                            switch (item.actionType)
                            {
                                case ActionType.Broadsword:
                                    player.animator.SetTrigger("Broadsword");
                                    break;
                                case ActionType.Shortsword:
                                    break;
                            }
                        }
                        break;
                    case ItemType.Block:
                        if (Input.GetMouseButtonDown(0))
                        {
                            // 마우스 커서 위치에서 타일 좌표 가져오기
                            Vector3 mousePosition = Input.mousePosition;
                            Vector3Int tilePosition = player.blockMap.WorldToCell(Camera.main.ScreenToWorldPoint(mousePosition));

                            // 타일 설정
                            if (player.blockMap.GetTile(tilePosition) == null)
                                if (player.blockMap.GetTile(tilePosition + Vector3Int.left) != null ||
                                    player.blockMap.GetTile(tilePosition + Vector3Int.right) != null ||
                                    player.blockMap.GetTile(tilePosition + Vector3Int.up) != null ||
                                    player.blockMap.GetTile(tilePosition + Vector3Int.down) != null
                                    )
                                {
                                    player.blockMap.SetTile(tilePosition, item.tile);
                                    player.animator.SetTrigger("Swing2");
                                }
                        }
                        break;
                    case ItemType.Wall:
                        if (Input.GetMouseButtonDown(0))
                        {
                            // 마우스 커서 위치에서 타일 좌표 가져오기
                            Vector3 mousePosition = Input.mousePosition;
                            Vector3Int tilePosition = player.wallMap.WorldToCell(Camera.main.ScreenToWorldPoint(mousePosition));

                            // 타일 설정
                            if (player.wallMap.GetTile(tilePosition) == null)
                            {
                                player.wallMap.SetTile(tilePosition, item.tile);
                                player.animator.SetTrigger("Swing2");
                            }
                        }
                        break;
                    case ItemType.Potion:
                        break;
                    case ItemType.Torch:
                        break;
                }
            }
        }
    }

    private class DeadState : BaseState
    {
        public DeadState(StateMachine fsm, PlayerAction player) : base(fsm, player) { }
        public override void Action()
        {

        }
    }
}
#endregion
