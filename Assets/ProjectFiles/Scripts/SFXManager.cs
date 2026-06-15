using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFiles.Scripts
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager Instance;

        //clase del tipo de sonido (en el enum) con sus variantes y características
        [System.Serializable]
        public class SFXEntry
        {
            public SFXType type;
            public AudioClip[] clips;
            [Range(0, 1)]
            public float volume = 0.7f;
        }

        //array de las entradas de la clase de arriba editables en el editor
        [SerializeField] private SFXEntry[] m_sfxEntries;
        [SerializeField] private int m_poolSize = 10; //cuántos puedo reproducir a la vez
        //el "array" que se crea en runtime de lo definido arriba
        private Dictionary<SFXType, SFXEntry> m_sfxMap;
        private AudioSource[] m_pool;
        private int m_poolIndex = 0;

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            //relleno el diccionario con las entradas de arriba
            m_sfxMap = new Dictionary<SFXType, SFXEntry>();
            foreach (SFXEntry entry in m_sfxEntries)
            {
                m_sfxMap[entry.type] = entry;
            }
            
            //crea una cantidad m_poolsize de audiosources 
            m_pool = new AudioSource[m_poolSize];
            for (int i = 0; i < m_pool.Length; i++)
            {
                m_pool[i] = gameObject.AddComponent<AudioSource>();
                m_pool[i].playOnAwake = false;
            }
        }

        //según el 'label' que le pasan otros scripts, lo busca y reproduce según sus características
        public void Play(SFXType type)
        {
            if (!m_sfxMap.ContainsKey(type)) return;
            SFXEntry entry = m_sfxMap[type];
            if (entry.clips.Length == 0)  return;
            AudioClip clip = entry.clips[Random.Range(0, entry.clips.Length)];
            AudioSource source = GetNextSource();
            source.clip = clip;
            source.volume = entry.volume;
            source.Play();
        }

        //selecciona el primer audiosource disponible en el editor
        private AudioSource GetNextSource()
        {
            AudioSource source = m_pool[m_poolIndex];
            m_poolIndex = (m_poolIndex + 1) % m_poolSize;
            return source;
        }
    }
}