using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//enum in-file porque solo lo referencia un script
public enum MusicTrack
{
    MainMenu,
    Level1,
    Level2,
    Boss,
    Victory
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [System.Serializable]
    public class MusicEntry
    {
        public MusicTrack track;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1;
    }
    
    [SerializeField] private MusicEntry[] m_tracks;
    [SerializeField] private float m_crossfadeDuration = 2f;
    
    private Dictionary<MusicTrack, MusicEntry> m_trackMap;
    private AudioSource m_sourceA;
    private AudioSource m_sourceB;
    private bool m_isSourceActive = true;

    //misma lógica que SFXManager
    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        m_sourceA = gameObject.AddComponent<AudioSource>();
        m_sourceB = gameObject.AddComponent<AudioSource>();
        m_sourceA.loop = true;
        m_sourceB.loop = true;
        
        m_trackMap = new Dictionary<MusicTrack, MusicEntry>();
        foreach (MusicEntry entry in m_tracks)
        {
            m_trackMap[entry.track] = entry;
        }
    }

    public void PlayTrack(MusicTrack track)
    {
        if (!m_trackMap.ContainsKey(track)) return;
        StartCoroutine(Crossfade(m_trackMap[track]));
    }

    private IEnumerator Crossfade(MusicEntry entry)
    {
        //el audiosource que esté activo se apaga y el otro se prende
        AudioSource fadeOut = m_isSourceActive ? m_sourceA : m_sourceB;
        AudioSource fadeIn = m_isSourceActive ? m_sourceB : m_sourceA;
        
        fadeIn.clip = entry.clip;
        fadeIn.volume = 0f;
        fadeIn.Play();

        float start = 0f;
        float startVolume = fadeOut.volume;

        //mientras pase el tiempo hasta la duración del fade, bajo el volumen de uno y subo el otro
        while (start < m_crossfadeDuration)
        {
            start +=  Time.deltaTime;
            float t = start / m_crossfadeDuration;
            fadeOut.volume = Mathf.Lerp(startVolume, 0f, t);
            fadeIn.volume = Mathf.Lerp(0f, entry.volume, t);
            yield return null;
        }
        
        fadeOut.Stop();
        fadeOut.volume = 0f;
        m_isSourceActive = !m_isSourceActive;
    }
    
    //------------------------------carga de nivel

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //evento propio de unity al cambiar de escena
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //defino el track a reproducir según el nombre de la escena
        //hay que hacerlo así ya que el cambio es con un trigger, no GameManager
        switch (scene.name)
        {
            case "MainMenu": PlayTrack(MusicTrack.MainMenu); break;
            case "Level1": PlayTrack(MusicTrack.Level1); break;
            case "Level2": PlayTrack(MusicTrack.Level2); break;
            case "BossArena": PlayTrack(MusicTrack.Boss); break;
        }
    }
}
