using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEgg : MonoBehaviour
{
    public static float BottomY = -30;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position.y < BottomY)
        {
            Destroy(gameObject);
            var apScript = Camera.main.GetComponent<DragonPicker>();
            apScript.DragonEggDestroyed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;
        var rend = GetComponent<Renderer>();
        rend.enabled = false;
        audioSource.Play();
    }
}
