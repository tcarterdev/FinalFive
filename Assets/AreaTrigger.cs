using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaTrigger : MonoBehaviour
{
    public TMP_Text AreaTriggerUIName;
    public string areaName;
    public float textScreenDuration;
    public float textFadeDuration;

    public Color CLEARWHITE;

    public void Start()
    {
        // Initialize the text color to be fully transparent
        AreaTriggerUIName.color = CLEARWHITE;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TextFade());
        }
    }

    public IEnumerator TextFade()
    {
        // Set the text and reset the color to fully transparent
        AreaTriggerUIName.SetText(areaName);
        AreaTriggerUIName.color = CLEARWHITE;

        // Fade in
        float timer = 0f;
        while (timer < textFadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / textFadeDuration);
            AreaTriggerUIName.color = new Color(1, 1, 1, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Wait for the text screen duration
        yield return new WaitForSeconds(textScreenDuration);

        // Fade out
        timer = 0f;
        while (timer < textFadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / textFadeDuration);
            AreaTriggerUIName.color = new Color(1, 1, 1, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is completely faded out
        AreaTriggerUIName.color = CLEARWHITE;
    }
}
