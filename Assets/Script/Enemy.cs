using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D enemyRB;
    public LayerMask playerLayer;
    public bool isAlive;
    public float checkHeadDistance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, checkHeadDistance, playerLayer))
        {
            isAlive = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the raycast in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * checkHeadDistance);
    }
}
