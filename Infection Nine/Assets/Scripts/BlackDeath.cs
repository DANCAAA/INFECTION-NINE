using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDeath : Enfermedad {

    private float contador = 10;
    private bool paralisis;
    private float tiempoParalisis = 3;
    private Actor actor;
    private float velocidadOriginal;
    private float contadorPerderVelocidad;
    private float velocidadPerdida;

    private void Start()
    {
        onset = 0;
        timeUntilDeath = 0;
        reduccionVelocidad = 0;
        enfermedadActiva = false;
        //particulasContagio;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (enfermedadActiva)
        {
            contador -= Time.deltaTime;
            contadorPerderVelocidad -= Time.deltaTime;
        }

        if(contadorPerderVelocidad <= 0 && velocidadPerdida > 0.4f)
        {
            PerderVelocidad();
        }

        if (contador <= 0)
        {
            float numero = Random.Range(0f, 1f);
            if (numero <= 0.15)
            {
                paralisis = true;
            }
            contador = 10;
        }

        if (paralisis)
        {
            tiempoParalisis -= Time.deltaTime;
            Paralisis();
        }

        if (tiempoParalisis <= 0 && paralisis)
        {
            DesactivarParalisis();
        }
    }

    private void PerderVelocidad()
    {
        if (actor is IA)
        {
            IA actorIA = actor as IA;
            actorIA.navMesh.speed -= velocidadOriginal * 0.05f;
            velocidadPerdida += 0.05f;
        }

        if (actor is Player)
        {
            Player actorPlayer = actor as Player;
            actorPlayer.VelocidadMovimiento -= velocidadOriginal * 0.05f;
            velocidadPerdida += 0.05f;
        }
    }

    private void Paralisis()
    {
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

    private void DesactivarParalisis()
    {
        if (actor is IA)
        {
            IA actorIA = actor as IA;
            actorIA.navMesh.speed = velocidadOriginal;
            paralisis = false;
            tiempoParalisis = 3;
        }

        if (actor is Player)
        {
            Player actorPlayer = actor as Player;
            actorPlayer.VelocidadMovimiento = velocidadOriginal;
            paralisis = false;
            tiempoParalisis = 3;
        }
    }

    public override void ActivarEnfermedad()
    {
        enfermedadActiva = true;
        actor = gameObject.GetComponent<Actor>();
        if(actor is IA)
        {
            velocidadOriginal = actor.GetComponent<IA>().navMesh.speed;
        }
        else
        {
            velocidadOriginal = actor.GetComponent<Player>().VelocidadMovimiento;
        }

        actor.TiempoMorirActivo = true;
    }
}
