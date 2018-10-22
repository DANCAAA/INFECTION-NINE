using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Survivor : IA {

    [SerializeField]
    private Transform[] puntosTrayectoria;
    [SerializeField]
    private int indexposicion = 0;
    private float tiempoParaSiguienteBusqueda = 10f;
    private float tiempoMaximoDeBusqueda = 10f;
    private float tiempoDeBusqueda;
    [SerializeField]
    private bool buscando;
    [SerializeField]
    private GameObject vacunaBuscada;
    [SerializeField]
    private float areaDeBusqueda;
    private bool recogiendoVacuna;
    // Use this for initialization
    void Start()
    {
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        Moverse();
        tiempoDeBusqueda = tiempoMaximoDeBusqueda;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enfermedad)
        {
            paritulaVirusA.SetActive(false);
            paritulaVirusS.SetActive(false); paritulaBlack.SetActive(false);
        }

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

        if (Contagiado && tiempoParaSiguienteBusqueda > 0 && buscando == false)
        {
            tiempoParaSiguienteBusqueda -= Time.deltaTime;
        }

        if(tiempoParaSiguienteBusqueda <= 0 )
        {
            Buscar();
            buscando = true;
            
        }

        if(buscando)
        {
            tiempoDeBusqueda -= Time.deltaTime;
            areaDeBusqueda += Time.deltaTime / 2;
        }

        if(buscando && (tiempoDeBusqueda <= 0 || !Contagiado))
        {
            Moverse();
            buscando = false;
            tiempoDeBusqueda = 10f;
            tiempoParaSiguienteBusqueda = 10f;
        }

        Posicionactual();

      
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
        if ((transform.position.x == puntosTrayectoria[indexposicion].position.x) && (transform.position.z == puntosTrayectoria[indexposicion].position.z))
        {
            indexposicion += 1;
            Moverse();
        }
    }

    private void Buscar()
    {
        if (Enfermedad is VirusA)
        {
            vacunaBuscada = GameObject.FindGameObjectWithTag("VacunaA");
        }

        if (Enfermedad is VirusS)
        {
            vacunaBuscada = GameObject.FindGameObjectWithTag("VacunaS");
        }

        if (Enfermedad is BlackDeath)
        {
            vacunaBuscada = GameObject.FindGameObjectWithTag("VacunaBlack");
        }

        if (vacunaBuscada)
        {
            if (Vector3.Distance(vacunaBuscada.transform.position, gameObject.transform.position) <= areaDeBusqueda)
            {
                navMesh.SetDestination(vacunaBuscada.transform.position);
            }
        }
        else
        {
            Moverse();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject objeto = collision.gameObject;
        if (collision.gameObject.tag == "IA")
        {
            if (objeto.GetComponent<IA>().Contagiado && !Contagiado)
            {
                if (objeto.GetComponent<IA>().Enfermedad is VirusA)
                {
                    float probabilidad = Random.Range(0f, 1f);
                    if (probabilidad <= ProbabilidadVirusA)
                    {
                        Contagiado = true;
                        Enfermedad = gameObject.AddComponent<VirusA>();
                    }
                }

                if (objeto.GetComponent<IA>().Enfermedad is VirusS)
                {
                    float probabilidad = Random.Range(0f, 1f);
                    if (probabilidad <= ProbabilidadVirusS)
                    {
                        Contagiado = true;
                        Enfermedad = gameObject.AddComponent<VirusS>();
                    }
                }

                if (objeto.GetComponent<IA>().Enfermedad is BlackDeath)
                {
                    float probabilidad = Random.Range(0f, 1f);
                    if (probabilidad <= ProbabilidadBlackDeath)
                    {
                        Contagiado = true;
                        Enfermedad = gameObject.AddComponent<BlackDeath>();
                    }
                }
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            if (objeto.GetComponent<Player>().Contagiado && !Contagiado)
            {
                if (objeto.GetComponent<Player>().Enfermedad is VirusA)
                {
                    float probabilidad = Random.Range(0f, 1f);
                    if (probabilidad <= ProbabilidadVirusA)
                    {
                        Contagiado = true;
                        Enfermedad = gameObject.AddComponent<VirusA>();
                    }
                }

                if (objeto.GetComponent<Player>().Enfermedad is VirusS)
                {
                    float probabilidad = Random.Range(0f, 1f);
                    if (probabilidad <= ProbabilidadVirusS)
                    {
                        Contagiado = true;
                        Enfermedad = gameObject.AddComponent<VirusS>();
                    }
                }

                if (objeto.GetComponent<Player>().Enfermedad is BlackDeath)
                {
                    float probabilidad = Random.Range(0f, 1f);
                    if (probabilidad <= ProbabilidadBlackDeath)
                    {
                        Contagiado = true;
                        Enfermedad = gameObject.AddComponent<BlackDeath>();
                    }
                }
            }


        }

        if (collision.gameObject.tag == "VacunaA" && Enfermedad is VirusA && Contagiado)
        {
            objeto.GetComponent<Vaccine>().UsarVacuna(Enfermedad, this);
        }

        if (collision.gameObject.tag == "VacunaS" && Enfermedad is VirusS && Contagiado)
        {
            objeto.GetComponent<Vaccine>().UsarVacuna(Enfermedad, this);
        }

        if (collision.gameObject.tag == "VacunaBlack" && Enfermedad is BlackDeath && Contagiado)
        {
            objeto.GetComponent<Vaccine>().UsarVacuna(Enfermedad, this);
        }
    }
}
