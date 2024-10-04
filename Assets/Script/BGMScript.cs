using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    AudioSource BGM;

    [SerializeField] AudioClip NormalBGM;
    [SerializeField] AudioClip ClearBGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();

    }
}
