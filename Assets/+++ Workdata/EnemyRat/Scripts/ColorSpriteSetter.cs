using UnityEngine;


public class ColorSpriteSetter : MonoBehaviour

{
 public Color colorSprite;
 
 public float colorTime;
 
 SpriteRenderer[] allSprites;
 
 Shader[] defaultShaders;
 
 Color[] defaultColors;
 
 Shader textGUIShader;
 
 
 void Start()
 {
  textGUIShader = Shader.Find("GUI/Text Shader");
  
  allSprites = gameObject.GetComponentInChildren<SpriteRenderer[]>(); 
  defaultShaders = new Shader[allSprites.Length];
  defaultColors = new Color[allSprites.Length];
  for(int i = 0; i < allSprites.Length; i++)
  {
   defaultShaders[i] = allSprites[i]?.material?.shader;
   defaultColors[i] = allSprites[i].color;
  }
 }

 public void ColorObject()
 {
  TurnToColor();
  Invoke("TurnNormal", colorTime);
 }

 private void TurnToColor()
 {
  for (int i = 0; i < allSprites.Length; i++)
  {
   allSprites[i].material.shader = defaultShaders[i];
   allSprites[i].color = defaultColors[i];
  }
 }

 private void TurnNormal()
 {
  for (int i = 0; i < allSprites.Length; i++)
  {
   allSprites[i].material.shader = defaultShaders[i];
   allSprites[i].color = defaultColors[i];
  }
 }
 
}
