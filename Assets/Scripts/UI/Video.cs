using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Video : MonoBehaviour
{
    [field: SerializeField] float delay = 37;
    [field: SerializeField] float holdToSkipTime = 2;
    [field: SerializeField] Image fillImage;
    [field: SerializeField] Color fillColor;
    [field: SerializeField] TMP_Text text;
    [field: SerializeField] GameObject skipEmpty;
    private float buttonHeld = 0;
    void Start()
    {
        Invoke("Load", delay);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.E))
        {
            buttonHeld += Time.deltaTime;
            if (buttonHeld > holdToSkipTime)
                Load();

            UpdateUI();
        }
        else
        {
            buttonHeld = 0;
            if (skipEmpty.activeInHierarchy)
                skipEmpty.SetActive(false);
        }
    }
    
    private void Load()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    private void UpdateUI()
    {
        float fillAmount = buttonHeld / holdToSkipTime;

        fillImage.fillAmount = fillAmount;
        fillImage.color = fillColor;
        text.color = fillColor;

        if (!skipEmpty.activeInHierarchy)
            skipEmpty.SetActive(true);
    }
}
