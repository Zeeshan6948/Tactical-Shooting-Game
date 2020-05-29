using UnityEngine;
using System.Collections;

public class ReversePS : MonoBehaviour
{

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    public float TimeToReverse;
    public float TimeToDestory;


    bool Reversebool;
    Vector3 heading, Direction;
    float Distance;
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
        Invoke("activateReverse", TimeToReverse);
        Invoke("DestroyObjectThis", TimeToReverse + TimeToDestory);
    }

    void activateReverse()
    {
        Reversebool = true;
    }

    void DestroyObjectThis()
    {
        Reversebool = false;
        Destroy(this.gameObject);
    }

    void Reverse()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.main.maxParticles];
        int count = ps.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
            heading = LevelManager.playercordinate - particles[i].position;
            Distance = heading.magnitude;
            Direction = heading / Distance;
            particles[i].position = particles[i].position + (Direction/3);
            particles[i].startSize = particles[i].startSize - Time.deltaTime;
        }
        ps.SetParticles(particles, count);
    }

    private void Update()
    {
        if (Reversebool)
        {
            Reverse();
        }
    }
}