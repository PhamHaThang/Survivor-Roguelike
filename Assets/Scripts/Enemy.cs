using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Effects")]
    [SerializeField] private ParticleSystem deathVFX;
    [Header("Attack")]
    [SerializeField] private float playerDetectionRadius = 5f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRate = 2f;
    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private bool gizmos;
    [SerializeField] private GameObject player;
    private EnemyMovement movement;
    private float attackTimer;
    private bool isSpawning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (player == null) {
            Debug.Log("<color=yellow>WARNING:</color> No target found, Auto-destroying....");
            Destroy(gameObject);
            return;
        }
        movement = GetComponent<EnemyMovement>();

        StartSpawnSequence();
    }

    // Update is called once per frame
    void Update() {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackRate) {
            TryAttack();
        }
    }
    private void StartSpawnSequence() {
        // Spawning: Hide the renderer + Show the spawn indicator
        isSpawning = true;
        SetRenderersVisibility(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;

        // // Scale up & down the spawn indicator
        // // Finish => Show the enemy & Hide the spawn indicator
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
                .setLoopPingPong(4)
                .setOnComplete(() => {
                    SetRenderersVisibility(true);
                    isSpawning = false;
                    movement.StorePlayer(player);
                });
    }
    private void SetRenderersVisibility(bool visibility = true) {
        spriteRenderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }
    private void TryAttack() {
        if (isSpawning) return;
        float distancePlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distancePlayer <= playerDetectionRadius) {
            Attack();
        }
    }
    private void Attack() {
        // Attack
        Debug.Log("Attack");
        attackTimer = 0;
    }
    private void Die() {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void OnDrawGizmos() {
        if (!gizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
