using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [field: SerializeField] float minLoadingTime = 5f;
    [field: SerializeField] float loadingTextUpdateDelay = 5f;
    [field: SerializeField] TMP_Text loadingText;
    float currentLoadingTime = 0;
    int it = 0;
    bool canSkip = false;
    // Start is called before the first frame update
    void Start()
    {
        currentLoadingTime = minLoadingTime;
        StartCoroutine(LoadingText());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSkip)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MartinTesting");
            }
        }
    }

    public IEnumerator LoadingText()
    {
        while (currentLoadingTime > 0)
        {
            currentLoadingTime -= loadingTextUpdateDelay;
            it++;
            loadingText.text = $"Loading{ReturnDots()}";
            yield return new WaitForSeconds(loadingTextUpdateDelay);
        }
        loadingText.fontSize = 20;
        loadingText.text = $"Press Space to continue...";
        canSkip = true;
    }
    private string ReturnDots()
    {
        string dots = "";
        int x = it % 4;
        for (int i = 0; i < x; i++)
            dots += " .";
        return dots;
    }
}
