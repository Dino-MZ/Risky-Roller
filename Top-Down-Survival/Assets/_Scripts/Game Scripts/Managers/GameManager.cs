using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Timer
    private bool count = true;

    private float time = 0f;
    private float msec;
    private float sec;
    private float min;

    public static int timesFinshed = 0;
    public static float bestTime;

    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI BestTimerText;
    [SerializeField] private TextMeshProUGUI FinalTimerText;

    [SerializeField] private GameObject endScreen;

    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(StopWatch());
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.isDead && !gameEnded)
        {
            gameEnded = true;

            timesFinshed += 1;

            count = false;
            endScreen.SetActive(true);

            if (time > bestTime && timesFinshed > 1)
            {
                bestTime = time;
            }
            else if (timesFinshed < 2)
            {
                bestTime = time;
            }
            // Final Time
            float f_msec = (int)((time - (int)time) * 100);
            float f_sec = (int)(time % 60);
            float f_min = (int)(time / 60 % 60);

            FinalTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", f_min, f_sec, f_msec);

            // Best time
            float b_msec = (int)((bestTime - (int)bestTime) * 100);
            float b_sec = (int)(bestTime % 60);
            float b_min = (int)(bestTime / 60 % 60);

            BestTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", b_min, b_sec, b_msec);
        }
    }

    IEnumerator StopWatch()
    {
        while (count)
        {
            time += Time.deltaTime;
            msec = (int)((time - (int)time) * 100);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);

            TimerText.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, msec);

            yield return null;
        }
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(1);
    }
}
