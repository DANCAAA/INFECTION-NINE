using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enfermedad : MonoBehaviour {

    [SerializeField]
    protected float onset; 
    [SerializeField]
    protected float timeUntilDeath;
    [SerializeField]
    protected float reduccionVelocidad;
    [SerializeField]
    protected bool enfermedadActiva;
    [SerializeField]
    protected GameObject particulasContagio;

    public float Onset
    {
        get
        {
            return onset;
        }

    }

    public float TimeUntilDeath
    {
        get
        {
            return timeUntilDeath;
        }

    }

    public abstract void ActivarEnfermedad();
}
