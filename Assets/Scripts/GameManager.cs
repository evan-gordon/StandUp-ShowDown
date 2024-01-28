using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public AudioClip bgNoise;
    public AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.PlayOneShot(bgNoise);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

    }
}
