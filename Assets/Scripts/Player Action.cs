using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private StateMachine fsm;

    private int currentSlot;
    private Item currentItem;

    public Camera camera;

    private void Start()
    {
        currentSlot = 0;
        currentItem = null;

        // fsm = new StateMachine();
        // fsm.Init(this);
    }

    private void Update()
    {
        if (inventoryManager.current != currentSlot || inventoryManager.mainInventory[currentSlot].item != currentItem)
        {
            this.currentSlot = inventoryManager.current;
            this.currentItem = inventoryManager.mainInventory[currentSlot].item;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = camera.nearClipPlane;
            Ray ray = camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f))
            {
                Gizmos.DrawLine(ray.origin, hit.point);
                Vector3Int position = blockMap.WorldToCell(hit.point);

                if (mineLayer.Contain(hit.collider.gameObject.layer))
                {
                    // 타일 삭제
                    blockMap.SetTile(position, null);
                }
                // Raycast가 맞춘 물체의 이름 출력
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                // Raycast가 맞춘 물체의 태그 출력
                Debug.Log("Raycast hit tag: " + hit.collider.gameObject.tag);

                // Raycast가 맞춘 물체의 레이어 출력
                Debug.Log("Raycast hit layer: " + hit.collider.gameObject.layer);
            }
            else
            {
                // Raycast가 아무것도 맞추지 않았을 때 출력
                Debug.Log("Raycast did not hit anything");
            }
            Debug.Log("click");
        }
    }
    private void OnClick(InputValue value)
    {
        // fsm.Action(Vector3Int.FloorToInt(value.Get<Vector3>()));
        // Debug.Log("click");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3Int tilePosition;
            if (mineLayer.Contain(hit.collider.gameObject.layer))
            {
                tilePosition = blockMap.WorldToCell(hit.point);
                blockMap.SetTile(tilePosition, null);
            }
        }
    }

    public void CreateTile(Vector3Int position, Item item)
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            int layerMask = 1 << hit.collider.gameObject.layer;

            if ((layerMask & tilemapLayerMask) != 0)
            {
                Vector3Int position = tilemap.WorldToCell(hit.point);
                tilemap.SetTile(position, item.tileData.tile);
            }
        }
        */
    }

    public void EraseTile(Vector3Int position)
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            int layerMask = 1 << hit.collider.gameObject.layer;

            if ((layerMask & tilemapLayerMask) != 0)
            {
                Vector3Int position = tilemap.WorldToCell(hit.point);
                tilemap.ClearTile(position);
            }
        }
        */
    }

    /*
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
        }

        public void Action(Vector3Int position)
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
        protected StateMachine fsm;
        protected PlayerAction player;
        protected Item item => player.currentItem;

        public AliveState(StateMachine fsm, PlayerAction player) : base(fsm, player) {}
        public override void Action()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3Int tilePosition;
                if (player.mineLayer.Contain(hit.collider.gameObject.layer))
                {
                    tilePosition = player.blockMap.WorldToCell(hit.point);
                    player.blockMap.SetTile(tilePosition, null);
                }
            }
        }
    }
    #endregion
    */
}
