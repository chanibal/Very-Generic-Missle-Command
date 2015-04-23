using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour {

    public float maxVelocity = 10;

    public float life=10;

    public GameObject explosion;

    public ParticleSystem engine;

    public MeshRenderer body;


    void Start()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * maxVelocity;
    }


    void Update()
    {
        life -= Time.deltaTime;
        if (life < 0)
            Boom();
    }


    void Boom()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        //engine.transform.parent = null;
        engine.emissionRate = 0;
        body.enabled = false;
        this.enabled = false;
        Destroy(gameObject, engine.startLifetime);
    }

}
