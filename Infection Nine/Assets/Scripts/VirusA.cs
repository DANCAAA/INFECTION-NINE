using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusA : Enfermedad {

    private Actor actor;
    public void Start()
    {
        onset = 30;
        timeUntilDeath = 15;
        reduccionVelocidad = 10;
        enfermedadActiva = false;
        actor = gameObject.GetComponent<Actor>();
    }


    public override void ActivarEnfermedad()
    {
        enfermedadActiva = true;
        actor.TiempoMorirActivo = true;
        
    }
}
