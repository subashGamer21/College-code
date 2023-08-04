using UnityEngine;
using TMPro;

public class countdown : MonoBehaviour
{
    public TMP_Text progressText;
    public float loadingTime = 5f; // Total loading time in seconds
    public AnimationCurve loadingCurve;

    private void Start()
    {
        StartCoroutine(StartLoading());
    }

    private System.Collections.IEnumerator StartLoading()
    {
        float elapsedTime = 0f;
        while (elapsedTime < loadingTime)
        {
            float progress = loadingCurve.Evaluate(elapsedTime / loadingTime);
            UpdateProgressText(progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the progress is at 100% after the loading is complete
        UpdateProgressText(1f);
        progressText.text = "Starting";
        // Optionally, you can perform any post-loading actions here.
    }

    private void UpdateProgressText(float progress)
    {
        // Update the progress text
        progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
    }
}
