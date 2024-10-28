using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] float audioDistance;

    [SerializeField] AudioSource[] sfxList;
    [SerializeField] AudioSource[] bgmList;
    int bgmIndex;

    public bool isPlayingBGM = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayRandomBGM();
    }

    void Update()
    {
        if (!isPlayingBGM)
        {
            StopAllBGM();
        }
        else
        {
            if (!bgmList[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }

    public void PlaySFX(int index, Transform target = null)
    {
        if (sfxList[index].isPlaying)
            return;

        if (target != null && Vector2.Distance(PlayerManager.instance.player.transform.position, target.position) > audioDistance)
            return;

        if (index < sfxList.Length)
        {
            sfxList[index].pitch = Random.Range(0.8f, 1.2f);
            sfxList[index].Play();
        }
    }

    public void StopSFX(int index)
    {
        sfxList[index].Stop();
    }

    public void PlayBGM(int index)
    {
        StopAllBGM();

        bgmIndex = index;
        bgmList[index].Play();
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgmList.Length; i++)
        {
            bgmList[i].Stop();
        }
    }

    public void PlayRandomBGM()
    {
        int randomIndex = Random.Range(0, bgmList.Length);
        PlayBGM(randomIndex);
    }
}
