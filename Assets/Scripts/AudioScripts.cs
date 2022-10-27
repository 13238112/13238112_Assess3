using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioScripts : MonoBehaviour
{
    public Sounds[] sounds;
    public static AudioScripts instance;
    private void Awake()
    {
        //To future proof the changing in scenes, make sure there's only one Audio source 
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Check through all the arrays to play them when called, they are also able to be adjusted in the inspector
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //the play method allows me to play an audio from any script using this syntax Play("Audio_Name"); and also checks for misspelling
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sounds: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            PlayIntroWithoutInvoke();
        }
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            Invoke("PlayNormalState", 0.0f);
        }
    }


    void PlayIntroWithoutInvoke()
    {
        //This is for the start screen
        Play("Intro");
    }

    void PlayIntro()
    {
        //I'll play the intro at the start of the game
        Play("Intro");
        //I need to play the next clip after the Intro finishes
        Invoke("PlayNormalState", 10.0f);
    }

    void PlayNormalState()
    {
        Play("Normal State");
    }

    void PlayScaredState()
    {
        Play("Scared State");
    }

    void PlayDeadState()
    {
        Play("Dead State");
    }

    void PlayMoving()
    {
        Play("Moving");
    }
    void PlayEat()
    {
        Play("Eat");
    }
    void PlayDies()
    {
        Play("Dies");
    }
    void PlayWallCollide()
    {
        Play("Wall Collide");
    }

    /* void Start()
     {
         GetComponent<AudioSource>().loop = true;
         StartCoroutine(playEngineSound());
     }
     IEnumerator playEngineSound()
     {
         GetComponent<AudioSource>().clip = introClip;
         GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
         GetComponent<AudioSource>().clip = normalState;
         GetComponent<AudioSource>().Play();
     }
     */
}
