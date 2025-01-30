using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    
    private Color _color;

    private void Awake()
    {
        _color = _fadeImage.color;
    }

    public IEnumerator Fade(float duration, float waitAfterFadingDuration = 0f)
    {
        for (float i = 0; i <= duration; i+=.1f)
        {
            _color.a = i/duration;
            _fadeImage.color = _color;
            yield return new WaitForSeconds(.1f);
        }

        if (_fadeImage.color.a < 1f)
        {
            _color.a = 1f;
            _fadeImage.color = _color;
        }
        
        yield return new WaitForSeconds(waitAfterFadingDuration);
    }
}
