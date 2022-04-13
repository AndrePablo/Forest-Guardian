using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public string _Level1;
    public string _Level2;
    public string _Level3;

    public void LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3(){
        SceneManager.LoadScene("Level3");
    }

    public void ExitButton(){
        Application.Quit();
    }

    public void VictoryBack(){
        SceneManager.LoadScene("Menu");
    }
}
