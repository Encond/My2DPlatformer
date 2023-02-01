using UnityEngine;
using UnityEngine.UI;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private AudioSource _mainMusic;
        [SerializeField] private AudioSource _underGroundMusic;

        [Header("Player Effect Sounds")]
        [SerializeField] private AudioSource[] _audioSources;

        private float _musicVolume = 0.6f;
        
        public static SoundManager Instance;
        
        public bool _shouldPlayMusic = true;
        public bool _isPlayingMainMusic;

        private void Start() => PlayMainMusic();

        public void PlayMainMusic()
        {
            if (!(_underGroundMusic.volume <= 0)) return;
            
            _mainMusic.Play();
            _isPlayingMainMusic = true;
        }

        public void PlayUnderGroundMusic()
        {
            if (!(_mainMusic.volume <= 0)) return;
            
            _underGroundMusic.Play();
            _isPlayingMainMusic = false;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void StopMusic()
        {
            if (_mainMusic.isPlaying && _mainMusic.volume > 0)
            {
                _mainMusic.volume -= Mathf.Abs(Time.deltaTime);
                _underGroundMusic.volume += Mathf.Abs(Time.deltaTime);
            }
            else if (_underGroundMusic.isPlaying && _underGroundMusic.volume > 0)
            {
                _mainMusic.volume += Mathf.Abs(Time.deltaTime);
                _underGroundMusic.volume -= Mathf.Abs(Time.deltaTime);
            }

            if (!(_mainMusic.volume <= 0) && !(_underGroundMusic.volume <= 0)) return;
            
            _mainMusic.Stop();
            _underGroundMusic.Stop();
        }

        private void Update()
        {
            if (!_mainMusic.isPlaying && _isPlayingMainMusic)
                _mainMusic.Play();
            else if (!_mainMusic.isPlaying && !_isPlayingMainMusic && !_underGroundMusic.isPlaying)
                _underGroundMusic.Play();
        }

        public void MusicVolumeChanged(Slider slider)
        {
            if (_mainMusic.isPlaying)
                _mainMusic.volume = PlayerPrefs.GetFloat("MusicVolume", slider.value);
            else if (_underGroundMusic.isPlaying)
                _underGroundMusic.volume = PlayerPrefs.GetFloat("MusicVolume", slider.value);
        }

        public void EffectVolumeChanged(Slider slider)
        {
            foreach (var item in _audioSources)
                item.volume = PlayerPrefs.GetFloat("EffectsVolume", slider.value);
        }
    }
}