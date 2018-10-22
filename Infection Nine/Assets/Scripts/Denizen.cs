using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Denizen : IA {
    [SerializeField]
    private Transform[] puntosTrayectoria;
    [SerializeField]
    private int indexposicion = 0;
	// Use this for initialization
	void Start () {
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        Moverse();
        Contagiado = true;
	}

	// Update is called once per frame
	void Update () {

        if (Enfermedad is VirusA)
        {
            paritulaVirusA.SetActive(true);
        }
        else { paritulaVirusA.SetActive(false); }

        if (Enfermedad is VirusS)
        {
            paritulaVirusS.SetActive(true);
        }
        else { paritulaVirusS.SetActive(false); }

        if (Enfermedad is BlackDeath)
        {
            paritulaBlack.SetActive(true);
        }
        else { paritulaBlack.SetActive(false); }

        Posicionactual();

        if (Contagiado)
        {
            if (Enfermedad is VirusA)
            {
                TiempoContagiadoVirusA += Time.deltaTime;
                if (TiempoContagiadoVirusA >= Enfermedad.Onset)
                {
                    Enfermedad.ActivarEnfermedad();
                }
            }
            if (Enfermedad is VirusS)
            {
                TiempoContagiadoVirusS += Time.deltaTime;
                if (TiempoContagiadoVirusS >= Enfermedad.Onset)
                {
                    Enfermedad.ActivarEnfermedad();
                }
            }

            if (Enfermedad is BlackDeath)
            {
                TiempoContagiadoVirusA += Time.deltaTime;
                if (TiempoContagiadoBlack >= Enfermedad.Onset)
                {
                    Enfermedad.ActivarEnfermedad();
                }
            }
        }

        if (tiempoMorirActivo)
        {
            if (Enfermedad is VirusA)
            {
                tiempoParaMorir += Time.deltaTime;
                if (tiempoParaMorir >= Enfermedad.TimeUntilDeath)
                {

                    Destroy(gameObject);
                }
            }

            if (Enfermedad is VirusS)
            {
                tiempoParaMorir += Time.deltaTime;
                if (tiempoParaMorir >= Enfermedad.TimeUntilDeath)
                {

                    Destroy(gameObject);
                }
            }

            if (Enfermedad is BlackDeath)
            {
                tiempoParaMorir += Time.deltaTime;
                if (tiempoParaMorir >= Enfermedad.TimeUntilDeath)
                {

                    Destroy(gameObject);
                }
            }
        }
    }

    private void Moverse()
    {
        
        if (indexposicion == puntosTrayectoria.Length)
        {
            indexposicion = 0;
        }
        navMesh.SetDestination(puntosTrayectoria[indexposicion].position);
    }

    private void Posicionactual()
    {
        if((transform.position.x == puntosTrayectoria[indexposicion].position.x) && (transform.position.z == puntosTrayectoria[indexposicion].position.z))
        {
            indexposicion += 1;
            Moverse();
        }
    }
}
