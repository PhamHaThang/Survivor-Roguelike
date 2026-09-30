using System;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [Header("Elements")]
    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask layerMask;

    [Header("Animations")]
    [SerializeField] private float aimLerp = 12f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        AutoAim();
    }
    private Enemy GetClosestEnemy() {
        Enemy closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        if (enemies.Length <= 0) {
            return null;
        }
        float minDistance = range;
        for (int i = 0; i < enemies.Length; i++) {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);
            if (distanceToEnemy < minDistance) {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }
        if (closestEnemy == null) {
            return null;
        }
        return closestEnemy;
    }
    private void AutoAim() {
        Enemy closestEnemy = GetClosestEnemy();
        Vector3 targetUpVector = Vector3.up;
        if (closestEnemy != null)
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
