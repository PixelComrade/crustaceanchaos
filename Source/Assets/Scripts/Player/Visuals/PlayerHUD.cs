using UnityEngine;
using System.Collections;

public class PlayerHUD : PlayerHealth 
{
	public bool displayHealthPips = true;
	
	public Texture healthPip;
	public Texture healthPipPower;
	
	private float healthX = 0.0f;
	private float healthY = 0.0f;
	private float healthW = 0.1f;
	private float healthH = 0.075f;
	
	private float scrW;
	private float scrH;
	
	void OnGUI ()
	{
		scrW = Screen.width;
		scrH = Screen.height;
		
		if(displayHealthPips == true)
		{
			for(int i = 0; i < GetHealth(); i++)
			{
				if(CheckPower() == false)
					DrawHealth(healthPip, i);
				else
					DrawHealth(healthPipPower, i);
			}
		}
	}
	
	void DrawHealth (Texture pip, int input)
	{
		GUI.DrawTexture(
			new Rect(
				(scrW * healthX + ((scrW * healthW) * input)) + (scrW * 0.045f), 
				scrH * healthY + (scrH * 0.045f), 
				scrW * healthW, 
				scrH * healthH), 
			pip, ScaleMode.ScaleToFit);
	}
	
	public void ModHealth (int input)
	{
		SetHealth(input);
	}
	
	public int CheckHealth ()
	{
		return GetHealth();
	}
	
	public int CheckMaxHealth ()
	{
		return GetMaxHealth();
	}
}
