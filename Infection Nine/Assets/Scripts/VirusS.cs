using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusS : Enfermedad
{

    private float contador = 10;
    private bool paralisis;
    private float tiempoParalisis;
    private Actor actor;
    private float velocidadOriginal;

    public override void ActivarEnfermedad()
    {
        enfermedadActiva = true;
        actor = gameObject.GetComponent<Actor>();
        if (actor is IA)
        {
            velocidadOriginal = actor.GetComponent<IA>().navMesh.speed;
        }
        else
        {
            velocidadOriginal = actor.GetComponent<Player>().VelocidadMovimiento;
        }
        actor.TiempoMorirActivo = true;
    }

    private void Start()
    {
        onset = 30;
        timeUntilDeath = 20;
        reduccionVelocidad = 0.2f;
        enfermedadActiva = false;
        //particulasContagio =
    }

    // Update is called once per frame
    void Update()
    {


        if (enfermedadActiva)
        {
            contador -= Time.deltaTime;
        }

        if (contador <= 0)
        {
            float numero = Random.Range(0f, 1f);
            if (numero <= 0.05)
            {
                paralisis = true;
            }
            contador = 10;
        }

        if (paralisis)
        {
            tiempoParalisis -= Time.deltaTime;
            if (actor is IA)
            {

                IA actorIA = actor as IA;
                
                actorIA.navMesh.speed = 0;
            }

            if (actor is Player)
            {
                Player actorPlayer = actor as Player;
               
                actorPlayer.VelocidadMovimiento = 0;
            }
        }

        if (tiempoParalisis <= 0 && paralisis)
        {
            if (actor is IA)
            {
                IA actorIA = actor as IA;               
                actorIA.navMesh.speed = velocidadOriginal;
                paralisis = false;
                tiempoParalisis = 2;
            }

            if (actor is Player)
            {
                Player actorPlayer = actor as Player;
                actorPlayer.VelocidadMovimiento = velocidadOriginal;
                paralisis = false;
                tiempoParalisis = 2;
            }
        }
    }
}
