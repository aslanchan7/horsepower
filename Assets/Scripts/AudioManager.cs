using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource RandomBabble;

    [SerializeField] private AudioSource SFXObject;
    [SerializeField] private AudioSource LoopObject;
    [SerializeField] private AudioSource RacingMusicSource;
    [SerializeField] private AudioClip CountdownClip;
    [SerializeField] private AudioClip RunningClip;
    [SerializeField] private AudioClip HorseFallClip;
    [SerializeField] private AudioClip BooClip;
    [SerializeField] private float gbmVolume;
    public bool raceStarted;


    private AudioSource racingMusicInstance;
    private AudioSource runningSfxInstance;
    private int frameCounter = 0;
    private int babbleDelay = 1000;


    private void Awake()
    {
        if (instance == null)
        {
            AudioSource musicInstance = Instantiate(RacingMusicSource, transform.position, Quaternion.identity);
            racingMusicInstance = musicInstance;
            racingMusicInstance.volume = gbmVolume;


            instance = this;
        }
    }

    private void Update()
    {
        frameCounter = frameCounter + 1;

        if (frameCounter == babbleDelay)
        {
            RandomBabble.Play();
            babbleDelay = Random.Range(2500,5000);
            frameCounter = 0;
        }
    }


    public void StartRace()
    {
        AudioSource IntroSfx = Instantiate(SFXObject, transform.position, Quaternion.identity);
        IntroSfx.clip = CountdownClip;
        IntroSfx.Play();
        Invoke("StartGBM", 3.0f);

        AudioSource runningInstance = Instantiate(LoopObject, transform.position, Quaternion.identity);
        runningSfxInstance = runningInstance;
        runningSfxInstance.clip = RunningClip;
        runningSfxInstance.volume = 0.1f;

        Invoke("StartRunningSfx", 3.0f);

        Destroy(IntroSfx.gameObject, IntroSfx.clip.length);
    }
    public void HorseFall()
    {
        PlaySFX(HorseFallClip, transform, 1.0f);
        PlaySFX(BooClip, transform, 1.0f);
        StartCoroutine(FadeOutAndStop(racingMusicInstance, 0.1f)); // short fade
        StartCoroutine(FadeOutAndStop(runningSfxInstance, 0.1f)); // short fade


    }
    public void HorseGetUp()
    {
        runningSfxInstance.Play();
        racingMusicInstance.Play();
    }
    public void PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(SFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.Play();

        Destroy(audioSource.gameObject, 5.0f);

    }
    private void StartRunningSfx()
    {
        runningSfxInstance.Play();
    }
    private void StartGBM()
    {
        racingMusicInstance.Play();
    }


    private IEnumerator FadeOutAndStop(AudioSource source, float fadeDuration)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // reset in case you reuse it
    }

}


