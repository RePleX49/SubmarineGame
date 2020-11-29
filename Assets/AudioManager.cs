using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> musicSources = new List<AudioSource>();
    public List<AudioSource> sfxSources = new List<AudioSource>();
    public float musicVolume = 1;
    public float sfxVolume = 1;
    public List<float> musicBaseVolumes = new List<float>();
    public List<float> sfxBaseVolumes = new List<float>();
    public Slider sfxSlider;
    public Slider musicSlider;


    // Start is called before the first frame update
    private void Awake()
    {

        //if (Systems.randomSeeding != null && Systems.randomSeeding != this)
        //    Destroy(this.gameObject);
        //else
        //    Systems.randomSeeding = this;
        DontDestroyOnLoad(gameObject);
        musicSlider = GameObject.FindWithTag("Slider").GetComponent<Slider>();
        sfxSlider = GameObject.FindWithTag("Slider2").GetComponent<Slider>();
        SetupVolumes();
        SetupVolumes();

    }

    private void OnLevelWasLoaded(int level)
    {
        if (level < 3)
        {
            musicSlider = GameObject.FindWithTag("Slider").GetComponent<Slider>();
            sfxSlider = GameObject.FindWithTag("Slider2").GetComponent<Slider>();
            SetupVolumes();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetSFX(sfxSlider.value);
        SetMusic(musicSlider.value);
        SetBaseVolumes();
        UpdateVolumes();
    }

    public void SetupVolumes()
    {
        for (int i = 0; i < musicSources.Count; i++)
        {
            musicBaseVolumes.Add(musicSources[i].volume);
        }

        for (int i = 0; i < sfxSources.Count; i++)
        {
            sfxBaseVolumes.Add(sfxSources[i].volume);
        }
    }

    public void SetBaseVolumes()
    {
        for (int i = 0; i < musicSources.Count; i++)
        {
            musicBaseVolumes[i] = musicSources[i].volume;
        }

        for (int i = 0; i < sfxSources.Count; i++)
        {
            musicBaseVolumes[i] = sfxSources[i].volume;
        }
    }

    public void UpdateVolumes()
    {
        for (int i = 0; i < musicSources.Count; i++)
        {
            musicSources[i].volume = musicBaseVolumes[i] * musicVolume;
        }

        for (int i = 0; i < sfxSources.Count; i++)
        {
            sfxSources[i].volume = sfxBaseVolumes[i] * sfxVolume;
        }
    }

    public void SetSFX(float sfx)
    {
        sfxVolume = sfx;
    }

    public void SetMusic(float music)
    {
        musicVolume = music;
    }
}
