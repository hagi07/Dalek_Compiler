using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {

	public Texture2D colorSpace;
	public Texture2D alphaGradient;
	public string Title = "Color Picker";
	public Vector2 startPos = new Vector2(20, 20);
	public GameObject receiver;
	public string colorSetFunctionName = "OnSetNewColor";
	public string colorGetFunctionName = "OnGetColor";
	public bool useExternalDrawer = false;
	public int drawOrder = 0;

	private Color TempColor; 
	private Color SelectedColor;

	static ColorPicker activeColorPicker = null;
	public Text errores;
	public Vector2 posicionErroresTexto = new Vector2(20, 20);

	enum ESTATE
	{
		Hidden,
		Showed,
		Showing,
		Hidding
	}; 
	ESTATE mState = ESTATE.Hidden;
	
	int sizeFull = 200;
	int sizeHidden = 20;
	float animTime = 0.25f;
	float dt = 0;

	float sizeCurr = 0;
	float alphaGradientHeight = 16;

	GUIStyle titleStyle = null;
	Color textColor = Color.black;
	Texture2D txColorDisplay;

	string txtR, txtG, txtB, txtA;
	float valR=1, valG=1, valB=1, valA=1;
	
	public void NotifyColor(Color color)
	{
		SetColor(color);
		SelectedColor = color;
		UpdateColorEditFields(false);
		UpdateColorSliders(false);
	}

	void Start()
	{
		sizeCurr = sizeHidden;

		txColorDisplay = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		if(receiver)
		{
			receiver.SendMessage(colorGetFunctionName, this, SendMessageOptions.DontRequireReceiver);
		}
		errores.transform.position = posicionErroresTexto;
		errores.color = Color.red;
	}


	void OnGUI()
	{
		if(!useExternalDrawer)
		{
			_DrawGUI();
		}
	}

	void UpdateColorSliders(bool isFocused)
	{
		if(!isFocused)
		{
			valR = TempColor.r;
			valG = TempColor.g;
			valB = TempColor.b;
			valA = TempColor.a;
		}
		else
		{
			SetColor(new Color(valR, valG, valB, valA));
		}
	}

	void UpdateColorEditFields(bool isFocused)
	{
		try{
			errores.text = "";
		if(!isFocused)
		{
			txtR = (255 * TempColor.r).ToString();
			txtG = (255 * TempColor.g).ToString();
			txtB = (255 * TempColor.b).ToString();
			txtA = (255 * TempColor.a).ToString();
		}
		else
		{

			byte r = 0;
			byte g = 0;
			byte b = 0;
			byte a = 0;
			
				r = byte.Parse(txtR, System.Globalization.NumberStyles.Any);
			
			
				g = byte.Parse(txtG, System.Globalization.NumberStyles.Any);
			

				b = byte.Parse(txtB, System.Globalization.NumberStyles.Any);
			

				a = byte.Parse(txtA, System.Globalization.NumberStyles.Any);
			
			SetColor(new Color32(r, g, b, a));
			}}
		catch{
			if(string.IsNullOrEmpty(txtR)){errores.text = "No puede estar vacio el campo R."; return;}
			else{
				try{float.Parse(txtR);}catch{errores.text = "Se esperaba un numero en el campo R."; return;}
				try{int.Parse(txtR);}catch{errores.text = "Se esperaba un entero en el campo R."; return;}
				if(int.Parse(txtR) > 255){ errores.text = "Error en campo R: Solo se pueden valores entre 0 y 255."; return;}
			}			
			if(string.IsNullOrEmpty(txtG)){errores.text = "No puede estar vacio el campo G."; return;}
			else{
				try{float.Parse(txtG);}catch{errores.text = "Se esperaba un numero en el campo G."; return;}
				try{int.Parse(txtG);}catch{errores.text = "Se esperaba un entero en el campo G."; return;}
				if(int.Parse(txtG) > 255) {errores.text = "Error en campo G: Solo se pueden valores entre 0 y 255."; return;}
			}
			if(string.IsNullOrEmpty(txtB)){errores.text = "No puede estar vacio el campo B."; return;}
			else{
				try{float.Parse(txtB);}catch{errores.text = "Se esperaba un numero en el campo B."; return;}
				try{int.Parse(txtB);}catch{errores.text = "Se esperaba un entero en el campo B."; return;}
				if(int.Parse(txtB) > 255) {errores.text = "Error en campo B: Solo se pueden valores entre 0 y 255."; return;}
			}
			if(string.IsNullOrEmpty(txtA)){errores.text = "No puede estar vacio el campo A."; return;}
			else{
				try{float.Parse(txtA);}catch{errores.text = "Se esperaba un numero en el campo A."; return;}
				try{int.Parse(txtA);}catch{errores.text = "Se esperaba un entero en el campo A."; return;}
				if(int.Parse(txtA) > 255){ errores.text = "Error en campo A: Solo se pueden valores entre 0 y 255."; return;}
			}
		}
		
	}

	// Update is called once per frame
	public void _DrawGUI () 
	{
		if (titleStyle == null) {
			titleStyle = new GUIStyle (GUI.skin.label);
			titleStyle.normal.textColor = textColor;
		}

		Rect rectColorEdit = new Rect(startPos.x + sizeCurr + 10, startPos.y + 30, 40, 140);
		Rect rectColorSlider = new Rect(startPos.x + sizeCurr + 50, startPos.y + 30, 60, 140);

		//GUI.Label(new Rect(startPos.x + sizeCurr + 60, startPos.y, 200, 30), Title, titleStyle);

		//GUI.DrawTexture(new Rect(startPos.x + sizeCurr + 10, startPos.y + 100, 40, 20), txColorDisplay);

		GameObject x = GameObject.Find ("Canvas/Panel/SlotColor/M36Color");
		if (x != null) {
			Image img  = x.GetComponent<Image> ();
			img.color = new Color (valR, valG, valB, valA);
		}

		if(mState == ESTATE.Showed)
		{

			txtR = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 30, 40, 20), txtR, 3);
			txtG = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 60, 40, 20), txtG, 3);
			txtB = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 90, 40, 20), txtB, 3);
			txtA = GUI.TextField(new Rect(startPos.x + sizeCurr + 10, startPos.y + 120, 40, 20), txtA, 3);
			valR = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 35, 60, 20), valR, 0.0f, 1.0f);
			valG = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 65, 60, 20), valG, 0.0f, 1.0f);
			valB = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 95, 60, 20), valB, 0.0f, 1.0f);
			valA = GUI.HorizontalSlider(new Rect(startPos.x + sizeCurr + 50, startPos.y + 125, 60, 20), valA, 0.0f, 1.0f);


			if(GUI.Button(new Rect(startPos.x + sizeCurr + 10, startPos.y + 150, 60, 20), "Ok"))
			{
				ApplyColor();
				SelectedColor = TempColor;
				if(receiver)
				{
					receiver.SendMessage(colorSetFunctionName, SelectedColor, SendMessageOptions.DontRequireReceiver);
				}
			}
			GUIStyle labelStyleRGBA = new GUIStyle(GUI.skin.label);
			labelStyleRGBA.normal.textColor = Color.black;
			GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 30, 20, 20), "R", labelStyleRGBA);
			GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 60, 20, 20), "G", labelStyleRGBA);
			GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 90, 20, 20), "B", labelStyleRGBA);
			GUI.Label(new Rect(startPos.x + sizeCurr + 110, startPos.y + 120, 20, 20), "A", labelStyleRGBA);
		}

		//update scaling states
		if(mState == ESTATE.Showing)
		{
			sizeCurr = Mathf.Lerp(sizeHidden, sizeFull, dt/animTime);
			if(dt/animTime > 1.0f) {
				mState = ESTATE.Showed;
			}
			dt += Time.deltaTime;
		}
		if(mState == ESTATE.Hidding)
		{
			sizeCurr = Mathf.Lerp(sizeFull, sizeHidden, dt/animTime);
			if(dt/animTime > 1.0f) {
				mState = ESTATE.Hidden;
			}
			dt += Time.deltaTime;
		}
		//draw color picker
		Rect rect = new Rect(startPos.x, startPos.y, sizeCurr, sizeCurr);
		GUI.DrawTexture(rect, colorSpace);

		float alphaGradHeight = alphaGradientHeight * (sizeCurr/sizeFull);
		Vector2 startPosAlpha = startPos + new Vector2(0, sizeCurr);
		Rect rectAlpha = new Rect(startPosAlpha.x, startPosAlpha.y, sizeCurr, alphaGradHeight);
		GUI.DrawTexture(rectAlpha, alphaGradient);

		Rect rectFullSize = new Rect(startPos.x, startPos.y, sizeCurr, sizeCurr + alphaGradHeight+100);

		Vector2 mousePos = Event.current.mousePosition;
		Event e = Event.current;
		bool isLeftMBtnClicked = e.type == EventType.mouseUp;
		bool isLeftMBtnDragging = e.type == EventType.MouseDrag;
		bool openCondition = (rectFullSize.Contains(e.mousePosition) && (((e.type == EventType.MouseUp || e.type == EventType.mouseDrag || e.type == EventType.MouseMove) && e.isMouse)));
		bool closeCondition = isLeftMBtnClicked || (!rectFullSize.Contains(e.mousePosition)) && (e.isMouse && (e.type == EventType.MouseMove || e.type == EventType.MouseDown));
		if(openCondition && (activeColorPicker == null || activeColorPicker.mState == ESTATE.Hidden))
		{
			if(mState == ESTATE.Hidden)
			{
				mState = ESTATE.Showing;
				activeColorPicker = this;
				dt = 0;
			}
		}
		if(closeCondition)
		{
			if(mState == ESTATE.Showed)
			{
				if(isLeftMBtnClicked)
				{
					ApplyColor();
				}
				else
				{
					SetColor(SelectedColor);
				}

				mState = ESTATE.Hidding;
				dt = 0;
			}
		}
		if(mState == ESTATE.Showed)
		{
			if(rect.Contains(e.mousePosition))
			{
				float coeffX = colorSpace.width / sizeCurr;
				float coeffY = colorSpace.height / sizeCurr;
				Vector2 localImagePos = (mousePos - startPos);
				Color res = colorSpace.GetPixel((int)(coeffX * localImagePos.x), colorSpace.height - (int)(coeffY * localImagePos.y)-1);
				SetColor(res);
				if(isLeftMBtnDragging )
				{
					ApplyColor();
				}
				UpdateColorEditFields(false);
				UpdateColorSliders(false);
			}
			else if(rectAlpha.Contains(e.mousePosition))
			{
				float coeffX = alphaGradient.width / sizeCurr;
				float coeffY = alphaGradient.height / sizeCurr;
				Vector2 localImagePos = (mousePos - startPosAlpha);
				Color res = alphaGradient.GetPixel((int)(coeffX * localImagePos.x), colorSpace.height - (int)(coeffY * localImagePos.y)-1);
				Color curr = GetColor();
				curr.a = res.r;
				SetColor(curr);
				if(isLeftMBtnDragging)
				{
					ApplyColor();
				}
				UpdateColorEditFields(false);
				UpdateColorSliders(false);
			}
			else if(rectColorEdit.Contains(e.mousePosition))
			{
				UpdateColorEditFields(true);
				UpdateColorSliders(false);
			}
			else if(rectColorSlider.Contains(e.mousePosition))
			{
				UpdateColorEditFields(false);
				UpdateColorSliders(true);
			}
			else
			{
				SetColor(SelectedColor);

			}
		}
	}

	public void SetColor(Color color)
	{
		TempColor = color;
		if(txColorDisplay != null)
		{
			txColorDisplay.SetPixel(0, 0, color);
			txColorDisplay.Apply();
		}
	}

	public Color GetColor()
	{
		return TempColor;
	}

	public void SetTitle(string title, Color textColor)
	{
		this.Title = title;
		this.textColor = textColor;
	}

	public void ApplyColor()
	{
		SelectedColor = TempColor;
		if(receiver)
		{
			receiver.SendMessage(colorSetFunctionName, SelectedColor, SendMessageOptions.DontRequireReceiver);
		}
	}
}
