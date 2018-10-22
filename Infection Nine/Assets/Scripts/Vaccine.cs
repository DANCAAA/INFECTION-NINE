using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : MonoBehaviour {

    [SerializeField]
    private int tipo;
    [SerializeField]
    private float inmunidadOfrecida;

    public int Tipo
    {
        get
        {
            return tipo;
        }
    }

    public void UsarVacuna(Enfermedad enfermedad, Actor actor)
    {
        if(enfermedad is VirusA && tipo == 0)
        {
            if (actor.inmunidadOcumuladaA < 0.5)
            {
                actor.Contagiado = false;
                actor.TiempoContagiadoVirusA = enfermedad.Onset * 0.1f;
                actor.TiempoMorirActivo = false;
                actor.TiempoParaMorir = 0;
                actor.inmunidadOcumuladaA += 0.1f;
                if (actor.ProbabilidadVirusA > 0.4f)
                {
                    actor.ProbabilidadVirusA -= inmunidadOfrecida;
                }
                Destroy(actor.GetComponent<Enfermedad>());
            }
        }

        if (enfermedad is VirusS && tipo == 1)
        {
            if (actor.inmunidadOcumuladaS < 0.5)
            {
                actor.Contagiado = false;
                actor.TiempoContagiadoVirusS = enfermedad.Onset * 0.1f;
                actor.TiempoMorirActivo = false;
                actor.TiempoParaMorir = 0;
                actor.inmunidadOcumuladaS += 0.1f;
                if (actor.ProbabilidadVirusS > 0.4f)
                {
                    actor.ProbabilidadVirusS -= inmunidadOfrecida;
                }
                Destroy(actor.GetComponent<Enfermedad>());
            }
        }

        if (enfermedad is BlackDeath && tipo == 2)
        {
            if (actor.inmunidadOcumuladaBlack < 0.5)
            {
                actor.Contagiado = false;
                actor.TiempoContagiadoBlack = enfermedad.Onset * 0.1f;
                actor.inmunidadOcumuladaBlack += 0.1f;
                actor.TiempoParaMorir = 0;
                actor.TiempoMorirActivo = false;
                if (actor.ProbabilidadBlackDeath > 0.4f)
                {
                    actor.ProbabilidadBlackDeath -= inmunidadOfrecida;
                }
                Destroy(actor.GetComponent<Enfermedad>());
            }
        }

        GameObject.Destroy(gameObject);
    }
}
