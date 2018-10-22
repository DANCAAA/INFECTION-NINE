using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {
    [SerializeField]
    private Enfermedad enfermedad;
    protected bool tiempoMorirActivo;
    [SerializeField]
    protected float tiempoParaMorir;
    [SerializeField]
    private bool contagiado;
    private float probabilidadBlackDeath = 0.5f;
    private float tiempoContagiadoBlack;
    public float inmunidadOcumuladaA, inmunidadOcumuladaS, inmunidadOcumuladaBlack;
    private float tiempoContagiadoVirusA;
    [SerializeField]
    private float tiempoContagiadoVirusS;
    private float probabilidadVirusA = 0.8f;
    private float probabilidadVirusS = 0.60f;
    [SerializeField]
    protected GameObject paritulaVirusA;
    [SerializeField]
    protected GameObject paritulaVirusS;
    [SerializeField]
    protected GameObject paritulaBlack;


    public bool Contagiado
    {
        get
        {
            return contagiado;
        }

        set
        {
            contagiado = value;
        }
    }

    public float TiempoContagiadoVirusA
    {
        get
        {
            return tiempoContagiadoVirusA;
        }

        set
        {
            tiempoContagiadoVirusA = value;
        }
    }

    public float TiempoContagiadoVirusS
    {
        get
        {
            return tiempoContagiadoVirusS;
        }

        set
        {
            tiempoContagiadoVirusS = value;
        }
    }

    public float TiempoContagiadoBlack
    {
        get
        {
            return tiempoContagiadoBlack;
        }

        set
        {
            tiempoContagiadoBlack = value;
        }
    }

    public float ProbabilidadVirusA
    {
        get
        {
            return probabilidadVirusA;
        }

        set
        {
            probabilidadVirusA = value;
        }
    }

    public float ProbabilidadVirusS
    {
        get
        {
            return probabilidadVirusS;
        }

        set
        {
            probabilidadVirusS = value;
        }
    }

    public float ProbabilidadBlackDeath
    {
        get
        {
            return probabilidadBlackDeath;
        }

        set
        {
            probabilidadBlackDeath = value;
        }
    }

    public Enfermedad Enfermedad
    {
        get
        {
            return enfermedad;
        }

        set
        {
            enfermedad = value;
        }
    }

    public float TiempoParaMorir
    {
        get
        {
            return tiempoParaMorir;
        }

        set
        {
            tiempoParaMorir = value;
        }
    }

    public bool TiempoMorirActivo
    {
        get
        {
            return tiempoMorirActivo;
        }

        set
        {
            tiempoMorirActivo = value;
        }
    }
}
