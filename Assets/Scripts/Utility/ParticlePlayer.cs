using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem[] allParticle;
    void Start()
    {
        allParticle = GetComponentsInChildren<ParticleSystem>();
    }

    public void Play()
    {
        foreach(ParticleSystem ps in allParticle)
        {
            ps.Stop();
            ps.Play();
        }
    }
}
