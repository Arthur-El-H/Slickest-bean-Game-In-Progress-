using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnimationManager : MonoBehaviour
{
    [SerializeField] Animator comboAnimOne;
    [SerializeField] Animator comboAnimTwo;
    [SerializeField] Animator comboAnimThree;
    [SerializeField] Animator comboAnimFour;
    [SerializeField] Animator comboScreen;

    private delegate void AnimationToBeDelayed();

    public void ReactToTimedAction(int currentScore)
    {
        if(currentScore < 5)
        {
            comboAnimOne.Play("Kombo 1 - 5");
            comboAnimFour.Play("Kombo 1 - 5");
        }
        else if(currentScore == 5)
        {
            comboAnimTwo.Play("Kombo 1 - 5");
            comboAnimThree.Play("Kombo 1 - 5");
            RandomizeComboScreen();
            comboScreen.Play("Nice");
        }
        else if (currentScore < 10)
        {
            comboAnimOne.Play("Kombo 5-10");
            comboAnimFour.Play("Kombo 5-10");
        }
        else if (currentScore == 10)
        {
            comboAnimOne.Play("Kombo 5-10");
            comboAnimTwo.Play("Kombo 1 - 5");
            comboAnimFour.Play("Kombo 5-10");
            RandomizeComboScreen();
            comboScreen.Play("Epic");
        }
        else if (currentScore < 20)
        {
            comboAnimOne.Play("Kombo 5-10");
            comboAnimFour.Play("Kombo 10-20");
        }
        else if (currentScore == 20)
        {
            comboAnimOne.Play("Kombo 5-10");
            comboAnimFour.Play("Kombo 10-20"); 
            comboAnimTwo.Play("Kombo 10-20");
            comboAnimThree.Play("20+");
            RandomizeComboScreen();
            comboScreen.Play("Beantastic");
        }
        else
        {
            comboAnimThree.Play("20+");
            comboAnimOne.Play("Kombo 1 - 5");
            comboAnimFour.Play("Kombo 10-20");
        }
    }

    private void RandomizeComboScreen()
    {
        comboScreen.transform.position = new Vector3(Random.Range(-7f, 7f), comboScreen.transform.position.y, 0);
    }

    IEnumerator Delay(float delayTime, AnimationToBeDelayed animationToBeDelayed)
    {
        yield return new WaitForSeconds(delayTime);
        animationToBeDelayed();
    }
}
