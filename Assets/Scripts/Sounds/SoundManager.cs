using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        // [SerializeField] private Sound[] _musicSource;
        // [SerializeField] private Sound[] _effectsSource;

        [SerializeField] private AudioSource _mainMusic;
        [SerializeField] private AudioSource _underGroundMusic;

        private float _mvol; // Global music volume
        private float _evol; // Global effects volume
        // private int _currentPlayingIndex = 999;

        public static SoundManager Instance;
        public bool _shouldPlayMusic;
        public bool _isPlayingMainMusic;

        private void Start()
        {
            PlayMainMusic();
        }

        public void PlayMainMusic()
        {
            _mainMusic.Play();
            _isPlayingMainMusic = true;
        }

        public void PlayUnderGroundMusic()
        {
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

            // CreateAudioSources(_effectsSource, 1f);
            // CreateAudioSources(_musicSource, 1f);
        }

        // private void CreateAudioSources(Sound[] sounds, float volume)
        // {
        //     foreach (Sound s in sounds)
        //     {
        //         s.source = gameObject.AddComponent<AudioSource>();
        //         s.source.clip = s.clip;
        //         s.source.volume = s.volume * volume;
        //         s.source.pitch = s.pitch;
        //         s.source.loop = s.loop;
        //     }
        // }

        // public void ChangeMusic(string name)
        // {
        //     if (_shouldPlayMusic == false)
        //     {
        //         _shouldPlayMusic = true;
        //         
        //         Sound s = Array.Find(_musicSource, sound => sound.name == name);
        //         if (s == null)
        //         {
        //             Debug.LogError("Unable to play sound " + name);
        //             return;
        //         }
        //
        //         s.source.Play();
        //     }
        // }

        // public void PlaySound(string name)
        // {
        //     Sound s = Array.Find(_effectsSource, sound => sound.name == name);
        //     if (s == null)
        //     {
        //         Debug.LogError("Unable to play sound " + name);
        //         return;
        //     }
        //
        //     s.source.Play();
        // }

        // public void PlayMusic()
        // {
        //     if (_shouldPlayMusic == false)
        //     {
        //         _shouldPlayMusic = true;
        //         _musicSource[0].source.Play();
        //
        //     _currentPlayingIndex = UnityEngine.Random.Range(0, _musicSource.Length - 1);
        //     _musicSource[_currentPlayingIndex].source.volume = _musicSource[0].volume * _mvol;
        //     _musicSource[_currentPlayingIndex].source.Play();
        // }
        // }

        public void StopMusic()
        {
            // _shouldPlayMusic = false;
            _mainMusic.Stop();
            _underGroundMusic.Stop();
            
            // _musicSource[0].source.Stop();
            // _musicSource[0].source.Stop();
            // if (_shouldPlayMusic == true)
            // {
            //     _shouldPlayMusic = false;
            //     _currentPlayingIndex = 999;
            // }
        }

        // void Update()
        // {
            // if we are playing a track from the playlist && it has stopped playing
            // if (_currentPlayingIndex != 999 && !_musicSource[_currentPlayingIndex].source.isPlaying)
            // {
            //     _currentPlayingIndex++; // set next index
            //     if (_currentPlayingIndex >= _musicSource.Length)
            //     {
            //         //have we went too high
            //         _currentPlayingIndex = 0; // reset list when max reached
            //     }
            //
            //     _musicSource[_currentPlayingIndex].source.Play(); // play that funky music
            // }
        // }

        // public String GetSongName()
        // {
        //     return _musicSource[_currentPlayingIndex].name;
        // }
        //
        // // if the music volume change update all the audio sources
        // public void MusicVolumeChanged(Slider slider)
        // {
        //     foreach (Sound m in _musicSource)
        //     {
        //         _mvol = PlayerPrefs.GetFloat("MusicVolume", slider.value);
        //         m.source.volume = _musicSource[0].volume * _mvol;
        //     }
        // }
        //
        // //if the effects volume changed update the audio sources
        // public void EffectVolumeChanged(Slider slider)
        // {
        //     _evol = PlayerPrefs.GetFloat("EffectsVolume", slider.value);
        //     foreach (Sound s in _effectsSource)
        //     {
        //         s.source.volume = s.volume * _evol;
        //     }
        //
        //     _effectsSource[0].source.Play(); // play an effect so user can her effect volume
        // }
    }
}