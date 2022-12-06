using Newtonsoft.Json.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnergyShield : MonoBehaviour
{
    public Action EggCaught;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        var mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        var mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        var pos = transform.position;
        pos.x = mousePos3D.x;
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collided = collision.gameObject;
        if (collided.tag == "Dragon Egg")
        {
            Destroy(collided);
            EggCaught?.Invoke();
            audioSource.Play();
        }
    }
}
