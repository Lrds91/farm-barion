using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    Player player;
    DayTimeController dayTime;

    private void Awake()
    {
        player = GetComponent<Player>();
        dayTime = GameManager.instance.timeController;
    }

    internal void DoSleep()
    {
        StartCoroutine(SleepRoutine());
    }

    IEnumerator SleepRoutine()
    {
        ScreenTint screenTint = GameManager.instance.screenTint;

        screenTint.Tint();
        yield return new WaitForSeconds(1f);

        player.FullHeal();
        //dayTime.SkipToMorning();

        screenTint.UnTint();
        yield return new WaitForSeconds(1f);

        yield return null;
    }
}
