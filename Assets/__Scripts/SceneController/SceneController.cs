using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public void Start_OnClick(){
      SceneManager.LoadScene("FirstScene");
  }
  public void Sound_OnClick(){
      SceneManager.LoadScene("Sound", LoadSceneMode.Additive);
  }
  public void UnloadSoundScene(){
     SceneManager.UnloadSceneAsync("Sound");
  }
}
