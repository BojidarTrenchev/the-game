using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    public List<ParticleSystem> particles;

    private ParticleSystem particle;

    //Any newly added particle as a child to this game object should be added also in this array, it doesn't happen automatically

    public ParticleSystem GetEffect(int numberInList)
    {
        return this.particles[numberInList];
    }

    public void PlayParticle(int indexOfParticle, Vector2 position)
    {
        particle = this.particles[indexOfParticle];
        particle.transform.position = position;

        if (!particle.gameObject.activeSelf)
        {
            particle.gameObject.SetActive(true);
        }

        particle.Play();
    }
}
