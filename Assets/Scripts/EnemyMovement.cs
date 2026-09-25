using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem deathVFX;
    [SerializeField] private float speed = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;

    private bool isSpawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (player == null) {
            Debug.Log("<color=yellow>WARNING:</color> No target found, Auto-destroying....");
            Destroy(gameObject);
        }
        // Spawning: Hide the renderer + Show the spawn indicator
        isSpawning = true;
        spriteRenderer.enabled = false;
        spawnIndicator.enabled = true;

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;

        // // Scale up & down the spawn indicator
        // // Finish => Show the enemy & Hide the spawn indicator
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
                .setLoopPingPong(4)
                .setOnComplete(() => {
                    spriteRenderer.enabled = true;
                    spawnIndicator.enabled = false;
                    isSpawning = false;
                });
    }

    // Update is called once per frame
    void Update() {
        FollowPlayer();
    }
    private void FollowPlayer() {
        if (isSpawning) return;
        Vector3 movementDirection = (player.transform.position - transform.position).normalized;

        transform.position += movementDirection * speed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (isSpawning) return;
        if (collision.gameObject.CompareTag("Player")) {
            Attack();
            Die();
        }
    }
    private void Attack() {
        // Attack
        Debug.Log("Attack");
    }
    private void Die() {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
