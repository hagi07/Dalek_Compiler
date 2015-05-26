using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dalek : MonoBehaviour {
	private GameObject energia;
	public Text texto;
    public GameObject padre;
    public Light luz;
    public ParticleRenderer agua;
    public ParticleRenderer fuego;
    public ParticleRenderer fuego1;
    public ParticleRenderer fuego2;
    public ParticleRenderer humo;
    public SkinnedMeshRenderer cuerpo;
    public MeshRenderer helices1;
    public MeshRenderer helices2;
    private GameObject suelo;
    private bool sueloOk;
    public Text errorLogico;
    public static bool fin;

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Suelo")
        {
            suelo = collision.gameObject;
            sueloOk = true;
        }
    }
	void OnTriggerEnter(Collider other) {
		energia = other.gameObject;

        if (other.name == "Kill")
            errorLogico.text = "Caíste del camino, no puedes llegar a tu destino.";

        if (other.tag == "Fin")
        {
            StartCoroutine(waitFin());
        }
	}

    IEnumerator waitFin() 
    {
        yield return new WaitForSeconds(1);
        fin = true;
        Programa.nivel++;
        Application.LoadLevel(Programa.nivel);
        Debug.Log(Programa.nivel);
    }
	void OnTriggerExit(Collider other) {
		energia = null;
	}
	public void quitarLuz()
	{
		if (energia != null) {
			Destroy(energia);
		}
		if (energia == null)
			texto.text = "No hay energia que recoger.";
	}
    public void avanzar() 
    {
        Vector3 target = padre.transform.position;
        if ((int)transform.rotation.eulerAngles.y >= 80 && (int)transform.rotation.eulerAngles.y <= 100)
            target = new Vector3(padre.transform.position.x + 20, padre.transform.position.y, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y >= -10 && (int)transform.rotation.eulerAngles.y <= 10)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z + 20);
        else if ((int)transform.rotation.eulerAngles.y >= 260 && (int)transform.rotation.eulerAngles.y <= 280)
            target = new Vector3(padre.transform.position.x - 20, padre.transform.position.y, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y >= 170 && (int)transform.rotation.eulerAngles.y <= 190)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z - 20);
        iTween.MoveTo(padre, target, .5f);
    }
    public void atras()
    {
        Vector3 target = transform.position;
        if ((int)transform.rotation.eulerAngles.y >= 80 && (int)transform.rotation.eulerAngles.y <= 100)
            target = new Vector3(padre.transform.position.x - 20, padre.transform.position.y, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y >= -10 && (int)transform.rotation.eulerAngles.y <= 10)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z - 20);
        else if ((int)transform.rotation.eulerAngles.y >= 260 && (int)transform.rotation.eulerAngles.y <= 280)
            target = new Vector3(padre.transform.position.x + 20, padre.transform.position.y, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y >= 170 && (int)transform.rotation.eulerAngles.y <= 190)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z + 20);
        iTween.MoveTo(padre, target, 1);
    }
    public void izquierda() 
    {
        Vector3 target = transform.position;
        if ((int)transform.rotation.eulerAngles.y >= 80 && (int)transform.rotation.eulerAngles.y <= 100)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z - 20);
        else if ((int)transform.rotation.eulerAngles.y >= -10 && (int)transform.rotation.eulerAngles.y <= 10)
            target = new Vector3(padre.transform.position.x - 20, padre.transform.position.y, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y >= 260 && (int)transform.rotation.eulerAngles.y <= 280)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z + 20);
        else if ((int)transform.rotation.eulerAngles.y >= 170 && (int)transform.rotation.eulerAngles.y <= 190)
            target = new Vector3(padre.transform.position.x + 20, padre.transform.position.y, padre.transform.position.z);
        iTween.MoveTo(padre, target, 1);
    }
    public void derecha()
    {
        Vector3 target = transform.position;
        if ((int)transform.rotation.eulerAngles.y >= 80 && (int)transform.rotation.eulerAngles.y <= 100)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z + 20);
        else if ((int)transform.rotation.eulerAngles.y >= -10 && (int)transform.rotation.eulerAngles.y <= 10)
            target = new Vector3(padre.transform.position.x + 20, padre.transform.position.y, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y >= 260 && (int)transform.rotation.eulerAngles.y <= 280)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y, padre.transform.position.z - 20);
        else if ((int)transform.rotation.eulerAngles.y >= 170 && (int)transform.rotation.eulerAngles.y <= 190)
            target = new Vector3(padre.transform.position.x - 20, padre.transform.position.y, padre.transform.position.z);
        iTween.MoveTo(padre, target, 1);
    }
    public void rotarDerecha() 
    {
        padre.transform.Rotate(new Vector3(0, -90, 0));
    }
    public void rotarIzquierda()
    {
        padre.transform.Rotate(new Vector3(0, 90, 0));
    }
    public void moverHelices() 
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("HelicesOn");
    }
    public void pararHelices() 
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("HelicesOff");
    }
    public void bailarOn() 
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("BailarOn");
    }
    public void bailarOff() 
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("BailarOff");
    }
    public void Clon()
    {
        GameObject x = Instantiate(padre);
        x.transform.GetChild(3).GetComponent<Rigidbody>().useGravity = false;
        x.transform.GetChild(3).GetComponent<BoxCollider>().enabled = false;
        x.transform.position = padre.transform.position;
        x.transform.rotation = Quaternion.identity;
        x.tag = "Eliminable";
    }
    public void dormir()
    {
        luz.enabled = false;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Dormir");
        transform.GetChild(8).GetComponent<SkinnedMeshRenderer>().materials[6].color = Color.black;
    }
    public void despertar() 
    {
        luz.enabled = true;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Despertar");
        transform.GetChild(8).GetComponent<SkinnedMeshRenderer>().materials[6].color = Color.white;
    }
    public void luzOn() 
    {
        luz.enabled = true;
    }
    public void luzOff() 
    {
        luz.enabled = false;
    }
    public void saltar() 
    {
        Vector3 target = transform.position;
        if ((int)transform.rotation.eulerAngles.y >= 80 && (int)transform.rotation.eulerAngles.y <= 100)
            target = new Vector3(padre.transform.position.x+20, padre.transform.position.y + 25, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y == 0)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y + 25, padre.transform.position.z + 20);
        else if ((int)transform.rotation.eulerAngles.y == 270)
            target = new Vector3(padre.transform.position.x - 20, padre.transform.position.y + 25, padre.transform.position.z);
        else if ((int)transform.rotation.eulerAngles.y == 0)
            target = new Vector3(padre.transform.position.x, padre.transform.position.y + 25, padre.transform.position.z - 20);
        if (sueloOk)
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
            iTween.MoveTo(padre, target, 1);
            StartCoroutine(wait());
            sueloOk = false;
            errorLogico.text = "";
        }
        else
            errorLogico.text = "No puedes saltar en el aire.";
    }
    private IEnumerator wait() 
    {
        yield return new WaitForSeconds(.5f);
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
    public void aguaG() 
    {
        if (agua.enabled)
            agua.enabled = false;
        else
            agua.enabled = true;
    }
    public void fuegoG()
    {
        if (fuego.enabled)
        {
            fuego.enabled = false;
            fuego1.enabled = false;
            fuego2.enabled = false;
        }
        else
        {
            fuego.enabled = true;
            fuego1.enabled = true;
            fuego2.enabled = true;
        }
    }
    public void humoG()
    {
        if (humo.enabled)
            humo.enabled = false;
        else
            humo.enabled = true;
    }
    public void invisible()
    {
        cuerpo.enabled = false;
        helices1.enabled = false;
        helices2.enabled = false;
    }
    public void visible()
    {
        cuerpo.enabled = true;
        helices1.enabled = true;
        helices2.enabled = true;
    }
    public void color(Color c) 
    {
        if (suelo != null) 
        {
            suelo.gameObject.GetComponent<MeshRenderer>().material.color = c;
        }
    }
}
