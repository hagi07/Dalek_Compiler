using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Programa : MonoBehaviour {
	public GameObject programa;
	public GameObject dalek;
    public GameObject padreDalek;
    public GameObject energia;
	public Text texto;
    public Text sintactico;
    public Text logico;
	public AudioSource bidobido;
	public AudioSource avast;
	public AudioSource ladrido;
	public AudioSource pam;
	public AudioSource saludo;
    public AudioSource adios;
	public AudioSource tambores;
	public AudioSource chillante;
    public AudioSource music;
    private int helices = 0;
	private int bailar =0;
	private int luz = 0;
	private int visible= 0;
	private int musica = 0;
	private int dormir = 0;
    private int ladrar = 0;
    private int alarma = 0;
	private bool ok;
	private int incremento;
	private int instrucciones;
    private int index;
	private Vector3 posicionInicialDalek = new Vector3(20,20,20);
    private Vector3 posicionInicialPadreDalek = new Vector3(20, 20, 20);
    public static float velocidadPorInstruccion;
    private Color color;
    public static int nivel = 0;

	void Awake()
	{
		texto.color = Color.blue;
		ok = false;
        posicionInicialDalek = dalek.transform.position;
        posicionInicialPadreDalek = padreDalek.transform.position;
        velocidadPorInstruccion = 1.1f;
        incremento = -1;
        Dalek.fin = false;
	}

	public void compilar()
	{
        if (string.IsNullOrEmpty(sintactico.text) && incremento == -1)
        {
            incremento = 0;
            padreDalek.transform.position = posicionInicialPadreDalek;
            dalek.transform.position = posicionInicialDalek;
            index = 0;
            instrucciones = 0;
            texto.text = "";
            /******************************************************************************************************/
            //										ANALISIS SEMANTICO											  //
            /******************************************************************************************************/
            helices = 0;
            bailar = 0;
            luz = 0;
            visible = 0;
            musica = 0;
            dormir = 0;
            ladrar = 0;
            alarma = 0;
            for (int i = 0; i < programa.transform.childCount; i++)
                if (programa.transform.GetChild(i) != null)
                    if (programa.transform.GetChild(i).childCount > 0)
                        if (programa.transform.GetChild(i).GetChild(0) != null)
                        {

                            if (programa.transform.GetChild(i).GetChild(0).name == "M5HeliceSi") helices++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M6HeliceNo") helices--;
                            if (helices < 0) { texto.text = "Las hélices no pueden apagarse antes de prenderse."; incremento = -1; return; }

                            if (programa.transform.GetChild(i).GetChild(0).name == "M7BaileSi") bailar++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M8BaileNo") bailar--;
                            if (bailar < 0) { texto.text = "No puedes parar de bailar sino has empezado."; incremento = -1; return; }

                            if (programa.transform.GetChild(i).GetChild(0).name == "M12EncenderLuz") luz++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M13ApagarLuz") luz--;
                            if (luz < 0) { texto.text = "No puedes apagar la luz sino la has prendido."; incremento = -1; return; }

                            if (programa.transform.GetChild(i).GetChild(0).name == "M19Invisible") visible++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M20Visible") visible--;
                            if (visible < 0) { texto.text = "No puedes aparecer si eres visible."; incremento = -1; return; }

                            if (programa.transform.GetChild(i).GetChild(0).name == "M25MusicaSi") musica++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M24MusicaNo") musica--;
                            if (musica < 0) { texto.text = "No puedes quitar la música si no está encendida."; incremento = -1; return; }

                            if (programa.transform.GetChild(i).GetChild(0).name == "M11Dormir") dormir++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M10Despertar") dormir--;
                            if (dormir < 0) { texto.text = "No puedes despertar si ya lo estas."; incremento = -1; return; }

                            if (programa.transform.GetChild(i).GetChild(0).name == "M32SonidoLadrido") ladrar++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M33SonidoLadridoNo") ladrar--;
                            if (ladrar < 0) { texto.text = "No puedes parar de ladrar si no estas ladrando."; incremento = -1; return; }

                            if (programa.transform.GetChild(i).GetChild(0).name == "M28SonidoAlarma") alarma++;
                            if (programa.transform.GetChild(i).GetChild(0).name == "M29SonidoAlarmaNo") alarma--;
                            if (alarma < 0) { texto.text = "No puedes detener la alarma si no está sonando."; incremento = -1; return; }

                            instrucciones++;
                        }
            /******************************************************************************************************/
            ok = true;
            if (instrucciones == 0)
            {
                incremento = -1;
                ok = false;
            }
        }
        else if (instrucciones == incremento)
            Application.LoadLevel(nivel);
	}

	void Update () {
        if (ok && incremento != instrucciones)
        {
            if (programa.transform.GetChild(index) != null)
            {
                if (programa.transform.GetChild(index).childCount > 0)
                {
                    if (programa.transform.GetChild(index).GetChild(0) != null)
                    {
                        if (programa.transform.GetChild(index).GetChild(0).name == "M36Color")
                            color = programa.transform.GetChild(index).GetChild(0).GetComponent<Image>().color;
                        BroadcastMessage(programa.transform.GetChild(index).GetChild(0).name);
                        StartCoroutine(pause());
                        programa.transform.GetChild(index).GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(0);
                        incremento++;
                    }
                    else
                        index++;
                }
                else
                    index++;
            }
            else
                index++;
            
        }
        if (instrucciones == incremento)
        {
                StartCoroutine(finWait());
        }
        Debug.Log(texto.text + "          " + incremento + "           " + sintactico.text);
	}

    IEnumerator finWait() 
    {
        yield return new WaitForSeconds(1.5f);
        if (!Dalek.fin)
            logico.text = "Programa terminado, Dalek no llegó a su destino.";
    }

	void reiniciar()
	{
		incremento = 0;
		for (int i =0; i < programa.transform.childCount; i++)
			if(programa.transform.GetChild (i) != null)
				if(programa.transform.GetChild (i).childCount > 0)
				if (programa.transform.GetChild (i).GetChild (0) != null) 
				{
					programa.transform.GetChild (i).GetChild (0).GetComponent<CanvasRenderer>().SetAlpha(1);
				}
		ok = false;
	}

	IEnumerator pause () 
	{ 
		ok = false;
        yield return new WaitForSeconds(velocidadPorInstruccion);
        index++;
		ok = true;
	}

	void errorLogico(string error)
	{
		texto.text = error;
	}
	void M1AvanzarFrente()
	{
        dalek.BroadcastMessage("avanzar");
	}
	void M2AvanzarAtras()
	{
        dalek.BroadcastMessage("atras");
	}
	void M3Derecha()
	{
        dalek.BroadcastMessage("derecha");
	}
	void M4Izquierda()
	{
        dalek.BroadcastMessage("izquierda");
	}
    void M5HeliceSi() 
    {
        dalek.BroadcastMessage("moverHelices");
    }
    void M6HeliceNo() 
    {
        dalek.BroadcastMessage("pararHelices");
    }
    void M7BaileSi() 
    {
        dalek.BroadcastMessage("bailarOn");
    }
    void M8BaileNo()
    {
        dalek.BroadcastMessage("bailarOff");
    }
	void M9Clon()
	{
        dalek.BroadcastMessage("Clon");
    }
    void M10Despertar() 
    {
        dalek.BroadcastMessage("despertar");
    }
    void M11Dormir()
    {
        dalek.BroadcastMessage("dormir");
    }
	void M12EncenderLuz()
	{
        Animator anim = dalek.GetComponent<Animator>();
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Dormir"))
            dalek.BroadcastMessage("luzOn");
	}
	void M13ApagarLuz()
	{
        dalek.BroadcastMessage("luzOff");
	}
	void M14EnergiaRecoger()
	{
		dalek.BroadcastMessage ("quitarLuz");
	}
	void M15EnergiaSoltar()
	{
		Instantiate (energia, dalek.transform.position + new Vector3(0,10,0), Quaternion.identity);
	}
    void M16FisicasAgua() 
    {
        dalek.BroadcastMessage("aguaG");
    }
    void M17FisicasFuego() 
    {
        dalek.BroadcastMessage("fuegoG");
    }
    void M18FisicasHumo() 
    {
        dalek.BroadcastMessage("humoG");
    }
    void M19Invisible() 
    {
        dalek.BroadcastMessage("invisible");
    }
    void M20Visible() 
    {
        dalek.BroadcastMessage("visible");
    }
	void M21Salto()
	{
        dalek.BroadcastMessage("saltar");
	}
	void M22GiraDerecha()
	{
        dalek.BroadcastMessage("rotarDerecha");
	}
	void M23GiraIzquierda()
	{
        dalek.BroadcastMessage("rotarIzquierda");  
	}
    void M24MusicaNo() 
    {
        music.mute = true;
    }
    void M25MusicaSi() 
    {
        music.mute = false;
        music.Play();
    }
    void M26SaludoAdios() 
    {
        adios.Play();
    }
    void M27SaludoHola() 
    {
        saludo.Play();
    }
	void M28SonidoAlarma()
	{
		bidobido.mute = false;
		bidobido.Play ();
	}
	void M29SonidoAlarmaNo()
	{
		bidobido.mute = true;
	}
	void M30SonidoAvast()
	{
		avast.Play ();
	}
	void M31SonidoChillante()
	{
		chillante.Play ();
	}
	void M32SonidoLadrido()
	{
		ladrido.mute = false;
		ladrido.Play ();
	}
	void M33SonidoLadridoNo()
	{
		ladrido.mute = true;
	}
	void M34SonidoPam()
	{
		pam.Play ();
	}
	void M35SonidoTambores()
	{
		tambores.Play ();
	}
	void M36Color()
	{
        dalek.BroadcastMessage("color",color);
	}
}
