using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private bool muted = false;

    void Start(){
        if (!PlayerPrefs.HasKey("muted")){
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else{
            Load();
        }
    }

    public void Sound_Off(){
        muted = true;
        AudioListener.pause = true;
        Save();
    }

    public void Sound_On(){
        muted = false;
        AudioListener.pause = false;
        Save();
    }

    private void Load(){
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save(){
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
