using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CantidadSliders : MonoBehaviour {
	string sliderTextString = "0";
	public Text sliderText;
    public AudioSource bidobido;
    public AudioSource avast;
    public AudioSource ladrido;
    public AudioSource pam;
    public AudioSource saludo;
    public AudioSource adios;
    public AudioSource tambores;
    public AudioSource chillante;
    public AudioSource musica;
    public Light luz;
    public Text errorLogico;
	
	public void textUpdate (float textUpdateNumber)
	{
		sliderTextString = ((int)textUpdateNumber).ToString();
		sliderText.text = sliderTextString;

        if (sliderText.name == "VolumenCant")
        { 
            float volumen = textUpdateNumber/100;
            bidobido.volume = volumen;
            avast.volume = volumen;
            ladrido.volume = volumen;
            pam.volume = volumen;
            saludo.volume = volumen;
            tambores.volume = volumen;
            chillante.volume = volumen;
            adios.volume = volumen;
            musica.volume = volumen;
            if (volumen == 0)
                errorLogico.text = "No podrás escuchar la música o los sonidos sin volumen.";
            else
                errorLogico.text = "";
        }
        if (sliderText.name == "VelocidadCant")
        {
            Programa.velocidadPorInstruccion = textUpdateNumber;
        }
        if (sliderText.name == "LuzCant") 
        {
            if (luz.enabled)
            {
                luz.intensity = textUpdateNumber;
                if (textUpdateNumber == 0)
                    errorLogico.text = "No podrás ver la luz si está al mínimo de intensidad.";
                else
                    errorLogico.text = "";
            }
        }
	}
}
