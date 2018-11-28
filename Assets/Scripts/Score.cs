using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text txt;
    public int intentos = 0;
    public int calificacion = 59;
    public int disponibles = 0;
    public int dificultad = 0;
    public string doorTag;
    public int gameStatus = 0; // 0: No iniciado - 1: Iniciado - 2: Finalizado
    
    // Use this for initialization
	void Start () {
		clearGame();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void setText(){
        txt.text = "Intentos:  " + intentos + ""+ " \nCalificación: " + calificacion;
    }

    public void clearText(){
        txt.text = "";
    }

    public void clearGame(){
        calificacion = 59;
        disponibles = 0;
        intentos = 0;
        dificultad = 0;
        gameStatus = 0;
    }

    public void UpdateScore(int i)
    {
        intentos += i;
    }
    
    public void setDificultad(int i)
    {
        dificultad = i;
        if(i == 1){
            disponibles = 4;
        }
        else if (i == 2){
            disponibles = 2;
        }
        else if(i == 3){
            disponibles = 1;
        }
        gameStatus = 1;
    }

    public void initGame(){
        int doorSelected = Random.Range(1,6);
        doorTag = "door"+doorSelected;
        Debug.Log(doorTag);
    }

    public void knockDoor(string tag)
    {
        if(gameStatus == 1){
            if(intentos < disponibles){
                UpdateScore(1);
                // the door is the one with the professor
                if(doorTag == tag){
                    gameStatus = 2;
                    switch (intentos)
                    {
                        case 1:
                            txt.text = "Encontraste al profesor. \nTu calificación es: 100";
                            break;
                        case 2:
                            txt.text = "Encontraste al profesor, pero te faltaron requerimientos :( \nTu calificación es: 90";
                            break;
                        case 3:
                            txt.text = "Entregaste después de la fecha límite. \nTu calificación es: 70";
                            break;
                        case 4:
                            txt.text = "¡Pasar es pasar! por poco no puedes entregar... \nTu calificación es: 60";
                            break;
                        default:
                            txt.text = "¿Como sucedió esto? \nTu calificación es: 60";
                            break;
                    }
                    txt.text += "\nPresiona el boton para jugar otra vez";
                }
                // the door isn't the correct
                else{
                    if(intentos == disponibles){
                        txt.text = "Oh no! No encontraste al profesor y reprobaste :( \nSuerte el siguiente semestre. \nTu calificación es: 59 \nPresiona el boton para jugar otra vez";
                        gameStatus = 2;
                    }
                    else{
                        txt.text = "Oh no! Ahí no se encuentra el profesor. \nIntentos:  " + intentos + "\nCalificación: " + calificacion;
                    }
                }
            }
        }
    }

}
