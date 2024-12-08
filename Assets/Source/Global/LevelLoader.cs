using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private const string Percent = "%";
    private const float Hundred = 100f;
    
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;
    
    private void Awake()
    {
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)SceneNames.Game);

        while (operation.isDone == false)
        {
            float value = Mathf.Clamp01(operation.progress / 0.9f);
            _slider.value = value;
            _text.SetText(value * Hundred + Percent);
            yield return null;
        }
    }
}
