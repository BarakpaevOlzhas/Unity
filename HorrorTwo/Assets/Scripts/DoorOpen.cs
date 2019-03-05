using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    private float openAngle, closeAngle, smooth;
    private bool IsLocked = true, isOpen = false;

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioClip doorOpen, doorClosed;

    void Start()
    {
        
    }

    public void Open()
    {
        if (IsLocked == false)
        {            
            isOpen = true;
            audio.clip = doorOpen;
            audio.Play();
        }
        else
        {
            audio.clip = doorClosed;
            audio.Play();
        }
    }

    public void Unlock()
    {
        IsLocked = false;
    }

    void Update()
    {
        if (isOpen)
        {     
            Quaternion targetRotationOpen = Quaternion.Euler(0, openAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationClosed = Quaternion.Euler(0, closeAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);
        }
    }
}
