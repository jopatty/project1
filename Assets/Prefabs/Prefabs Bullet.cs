using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //bullet travel spd
    [Range(1.10)]
    [SerializeField] private float speed = 10f;

    //destroy after second
    [Range(1.10)]
    [SerializeField] private float lifeTime = 4f;

    private Rigidbody2D rb;

    // Start is called before the  first frame update
    private void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
