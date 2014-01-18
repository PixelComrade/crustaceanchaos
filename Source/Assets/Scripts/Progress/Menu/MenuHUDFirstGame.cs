using UnityEngine;
using System.Collections;

public class MenuHUDFirstGame : MonoBehaviour 
{
	public Texture logo;
	public Texture storyBox;
	public Texture howtoBox;
	
	public Texture WinBoxConsume;
	public Texture WinBoxCollect;
	public Texture WinBoxDestroy;
	public Texture LoseBox;
	
	/* 
	 * Menu divided into 3 sections
	 * Top logo section
	 * Left button section
	 * Right info section
	 */
	
	private float logoX = 0.0f;
	private float logoY = 0.0f;
	private float logoW = 1.0f;
	private float logoH = 0.3f;	
		
	private float leftX = 0.0f;
	private float leftY = 0.3f;
	private float leftW = 0.4f;
	private float leftH = 0.7f;
			
	private float rightX = 0.4f;
	private float rightY = 0.3f;
	private float rightW = 0.6f;
	private float rightH = 0.7f;
	
	private float scrW;
	private float scrH;
	
	private int whichScreen = 0;
	
	private bool viewingHowTo = false;
	
	void OnGUI()
	{
		if(whichScreen != -1)
		{			
			scrW = Screen.width;
			scrH = Screen.height;

			// Buttons
			if(GUI.Button(
				new Rect(
					scrW * (leftX + (leftW * 0.75f) / 4), 
					scrH * leftY, 
					scrW * leftW * 0.75f, 
					scrH * (leftH / 3) * 0.5f
				),
				"Play"))
			{
				Debug.Log("Playing first time");
				Application.LoadLevel(1);
			}
			if(GUI.Button(
				new Rect(
					scrW * (leftX + (leftW * 0.75f) / 4), 
					scrH * leftY + (scrH * (leftH / 3)), 
					scrW * leftW * 0.75f, 
					scrH * (leftH / 3) * 0.5f
				),
				"How To"))
			{
				viewingHowTo = !viewingHowTo;
			}
			if(GUI.Button(
				new Rect(
					scrW * (leftX + (leftW * 0.75f) / 4), 
					scrH * leftY + ((scrH * (leftH / 3)) * 2), 
					scrW * leftW * 0.75f, 
					scrH * (leftH / 3) * 0.5f
				), 
				"Exit"))
			{
				Application.Quit();
			}
			
			// Textures
			GUI.DrawTexture(new Rect(scrW * logoX, scrH * logoY, scrW * logoW, scrH * logoH), logo, ScaleMode.StretchToFill);
			
			if(viewingHowTo == false)
			{
				if(whichScreen == 1)
					GUI.DrawTexture(new Rect(scrW * rightX, scrH * rightY, scrW * rightW, scrH * rightH), WinBoxConsume, ScaleMode.ScaleToFit);
				else if(whichScreen == 2)
					GUI.DrawTexture(new Rect(scrW * rightX, scrH * rightY, scrW * rightW, scrH * rightH), WinBoxCollect, ScaleMode.ScaleToFit);
				else if(whichScreen == 3)
					GUI.DrawTexture(new Rect(scrW * rightX, scrH * rightY, scrW * rightW, scrH * rightH), WinBoxDestroy, ScaleMode.ScaleToFit);
				else if(whichScreen == 4)
					GUI.DrawTexture(new Rect(scrW * rightX, scrH * rightY, scrW * rightW, scrH * rightH), LoseBox, ScaleMode.ScaleToFit);
				else
					GUI.DrawTexture(new Rect(scrW * rightX, scrH * rightY, scrW * rightW, scrH * rightH), storyBox, ScaleMode.ScaleToFit);
			}
			else
				GUI.DrawTexture(new Rect(scrW * rightX, scrH * rightY, scrW * rightW, scrH * rightH), howtoBox, ScaleMode.ScaleToFit);
		}
	}
}
