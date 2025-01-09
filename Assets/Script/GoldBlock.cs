using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBlock : MonoBehaviour
{
    public bool canHit;
    public SpriteRenderer spriteRen;
    public float checkDistance = 1f;
    public LayerMask playerLayer;
    void Start()
    {
        canHit = true;
        spriteRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, checkDistance, playerLayer))
        {
            if (canHit) 
            {
                canHit = false;
                spriteRen.color = Color.gray;
            }
        }
    }
}
