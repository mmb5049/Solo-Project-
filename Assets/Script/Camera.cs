using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    private Vector3 newPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // update position to the target
        newPos = new Vector3 (target.transform.position.x, target.transform.position.y, -10f);
        transform.position = newPos;
    }
}
