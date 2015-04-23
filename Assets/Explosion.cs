using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	void Start () {
        float maxLife = 0;
        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
            maxLife = Mathf.Max(maxLife, ps.duration);
        Destroy(gameObject, maxLife);
	}

}
