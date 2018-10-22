using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grudger : IA {

    [SerializeField]
    private float distancia, tiempoPerseguir, TiempoActualPersiguiendo;
    private GameObject player;
    private Vector3 posicionBase;

    // Use this for initialization
    void Start () {
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        TiempoActualPersiguiendo = tiempoPerseguir;
        posicionBase = gameObject.transform.position;
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

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= distancia && TiempoActualPersiguiendo > 0)
        {
            navMesh.SetDestination(player.transform.position);
            TiempoActualPersiguiendo -= Time.deltaTime;
        }

        if(TiempoActualPersiguiendo <= 0)
        {
            navMesh.SetDestination(posicionBase);
        }

        if ((transform.position.x == posicionBase.x) && (transform.position.z == posicionBase.z))
        {
            TiempoActualPersiguiendo = tiempoPerseguir;
        }
    }
}
