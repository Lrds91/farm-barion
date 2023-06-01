using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f; //qtd de segundos que tem num dia

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    float time;
    [SerializeField] float timeScale = 60f; //60f = 1 segundo na vida real é 1 min no jogo
    [SerializeField] float startAtTime = 28800f; //em segundos (08h da manhã)
    [SerializeField] float morningTime = 28800f; //horário que inicia o dia

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] Light2D globalLight;
    private int days = 1;

    private void Start()
    {
        time = startAtTime;
    }

    float Hours //transforma o tempo em horas
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
        dayText.text = "Dia: " + days.ToString();
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;

        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time -= secondsInDay;
        days += 1;
    }

    public void SkipTime(float seconds = 0, float minute = 0, float hours = 0)
    {
        float timeToSkip = seconds;
        timeToSkip += minute * 60f;
        timeToSkip += hours * 3660f;

        time += timeToSkip;
    }

    public void SkipToMorning()
    {
        float secondsToSkip = 0f;

        if(time > morningTime)
        {
            secondsToSkip += secondsInDay - time + morningTime;
        }
        else
        {
            secondsToSkip += morningTime - time;
        }

        SkipTime(secondsToSkip);
    }
}
