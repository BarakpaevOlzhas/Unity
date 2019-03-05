using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private Transform root;

    [SerializeField]
    private Transform screamer;

    void Start()
    {
        screamer.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            screamer.gameObject.SetActive(true);

            audio.Play();

            Invoke("Destroy", 3f);
        }
    }

    private void Destroy()
    {
        Destroy(root.gameObject);
    }
}
