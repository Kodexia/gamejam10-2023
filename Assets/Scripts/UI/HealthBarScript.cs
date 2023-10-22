using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [field: SerializeField] protected Gradient healthColors { get; private set; }
    [field: SerializeField] protected Canvas canvas { get; private set; }

    [field: SerializeField] protected Image fillSprite { get; private set; }

    public void UpdateHealthbar(float currentHp, float maxHp)
    {
        ShowHealthbar();

        float fillAmount = currentHp / maxHp;
        fillSprite.fillAmount = fillAmount;
        Color c = healthColors.Evaluate(fillAmount);
        fillSprite.color = c;
    }

    public void ShowHealthbar()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, GameManager.instance.playerBehaviour.transform.position);
        if (distanceFromPlayer < 5f)
        {
            if (!canvas.isActiveAndEnabled)
                canvas.enabled = true;
        }
        else
            if (canvas.isActiveAndEnabled)
                canvas.enabled = false;
    }
}