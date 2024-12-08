using UnityEngine;

public class Stopper : MonoBehaviour
{
    private void OnDestroy()
    {
        Application.focusChanged -= OnFocusChangedApp;
    }

    public void Initialize()
    {
        Application.focusChanged += OnFocusChangedApp;
    }

    public void Pause()
    {
        Time.timeScale = (float)ValueConstants.Zero;
        AudioListener.pause = true;
    }

    public void Release()
    {
        Time.timeScale = (float)ValueConstants.One;
        AudioListener.pause = false;
    }

    private void OnFocusChangedApp(bool isInApp)
    {
        if (isInApp == false)
            Pause();
        else
            Release();
    }
}