using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    float countDownTime = 3f;
    Coroutine currentCountDown;
    int comboCount;
    int maxCombo;
    [SerializeField] Text currentComboTxt;

    public void DoubleJumpWasTimed() 
    {
        actualize();
    }

    public void BeanJumpWasTimed() 
    {
        actualize();
    }

    public void WallJumpWasTimed() 
    {
        actualize();
    }

    public void BeanWasDestroyed()
    {
        actualize();
    }

    public void BouncedOffTheWall()
    {
        actualize();
    }

    private void actualize()
    {
        if (currentCountDown != null) StopCoroutine(currentCountDown);
        currentCountDown = StartCoroutine(actualizeCountDown());
        comboCount++;
        currentComboTxt.text = comboCount.ToString();
    }

    private IEnumerator actualizeCountDown()
    {
        yield return new WaitForSeconds(countDownTime);
        if (comboCount > maxCombo) maxCombo = comboCount;
        comboCount = 0;
        currentComboTxt.text = comboCount.ToString();
    }
}
