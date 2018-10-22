using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : Actor {

    public NavMeshAgent navMesh;

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
