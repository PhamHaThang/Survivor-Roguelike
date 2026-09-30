using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10f;
    private Player player;

    void Update() {
        if (player != null)
            FollowPlayer();
    }

    private void FollowPlayer() {
        Vector3 movementDirection = (player.transform.position - transform.position).normalized;

        transform.position += movementDirection * moveSpeed * Time.deltaTime;
    }

    public void StorePlayer(Player player) {
        this.player = player;
    }
}
