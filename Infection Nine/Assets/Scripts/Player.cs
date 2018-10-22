using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor {

    [SerializeField]
    private float velocidadMovimiento;
    private bool llave;
    [SerializeField]
    private GameObject gameover, win;

    public float VelocidadMovimiento
    {
        get
        {
            return velocidadMovimiento;
        }

        set
        {
            velocidadMovimiento = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (!Enfermedad)
        {
            paritulaVirusA.SetActive(false);
            paritulaVirusS.SetActive(false);paritulaBlack.SetActive(false);
        }

        if(Enfermedad is VirusA)
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


        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical")* VelocidadMovimiento * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * VelocidadMovimiento * Time.deltaTime);
        }

        if (tiempoMorirActivo)
        {
            if (Enfermedad is VirusA)
            {
                tiempoParaMorir += Time.deltaTime;
                if (tiempoParaMorir >= Enfermedad.TimeUntilDeath)
                {
                    gameover.SetActive(true);
                    velocidadMovimiento = 0;
                }
            }

            if (Enfermedad is VirusS)
            {
               tiempoParaMorir += Time.deltaTime;
                if (tiempoParaMorir >= Enfermedad.TimeUntilDeath)
                {
                    gameover.SetActive(true);
                    velocidadMovimiento = 0;
                }
            }

            if (Enfermedad is BlackDeath)
            {
                tiempoParaMorir += Time.deltaTime;
                if (tiempoParaMorir >= Enfermedad.TimeUntilDeath)
                {
                    gameover.SetActive(true);
                    velocidadMovimiento = 0;
                }
            }
        }

        if (Contagiado)
        {
            if(Enfermedad is VirusA)
            {
                TiempoContagiadoVirusA += Time.deltaTime;
                if(TiempoContagiadoVirusA>= Enfermedad.Onset)
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

    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject objeto = collision.gameObject;
        if(collision.gameObject.tag == "IA")
        {
            
            if (objeto.GetComponent<IA>().Contagiado && !Contagiado)
            {
                
                if(objeto.GetComponent<IA>().Enfermedad is VirusA)
                {
                    float probabilidad = Random.Range(0f, 1f);
                    
                    if(probabilidad <= ProbabilidadVirusA)
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

        if(collision.gameObject.tag == "VacunaA" && Enfermedad is VirusA && Contagiado) 
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

        if(collision.gameObject.tag== "Llave")
        {
            llave = true;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Win")
        {
            if(llave && !tiempoMorirActivo)
            win.SetActive(true);
        }
    }
}
