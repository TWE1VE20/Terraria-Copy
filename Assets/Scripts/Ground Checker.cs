using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Collider2D groundCollider;

    private bool isGround;
    public bool IsGround { get { return isGround; } }

    private void FixedUpdate()
    {
        isGround = CheckGround();
    }

    bool CheckGround()
    {
        Vector2[] rayDirections = new Vector2[] {
            Vector2.down + Vector2.right*2,
            Vector2.down + Vector2.left*2,
            Vector2.down
        };

        bool[] hitResults = new bool[3];
        for (int i = 0; i < 3; i++)
            hitResults[i] = Raycast(rayDirections[i]);

        for (int i = 0; i < 3; i++)
            if (hitResults[i])
                return true;
        return false;
    }

    bool Raycast(Vector2 direction)
    {
        Vector2 rayOrigin = groundCollider.bounds.center;
        float rayDistance = groundCollider.bounds.extents.y;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayDistance, groundLayer);

        if (hit.collider != null)
            return true;
        else
            return false;
    }

}
