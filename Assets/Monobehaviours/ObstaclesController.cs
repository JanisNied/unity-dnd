using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObstaclesController : MonoBehaviour
{
    [HideInInspector]
    public float speed = 1f;
    public float waveAmp = 25f;
    public float waveFreq = 1f;

    public float fadeDuration = 1.5f;

    private ObjScript _objectScript;
    private CarBounds _screenBounds;

    private CanvasGroup _group;
    private RectTransform _transform;
    private bool isFadingOut = false;

    private Image _image;

    private Color _orgColor;

    public float _fadeOffset = 80f;
    private void Start()
    {
        _group = GetComponent<CanvasGroup>();
        if (_group == null)
        {
            _group = gameObject.AddComponent<CanvasGroup>();
        }
        _transform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _orgColor = _image.color;
        _objectScript = Object.FindFirstObjectByType<ObjScript>();
        _screenBounds = Object.FindFirstObjectByType<CarBounds>();

        StartCoroutine(FadeIn());
    }
    private void Update()
    {
        float wave = Mathf.Sin(Time.time * waveFreq) * waveAmp;
        _transform.anchoredPosition += new Vector2(-speed * Time.deltaTime, wave * Time.deltaTime);
        // destroy if out of bounds to the left
        if (speed > 0 && transform.position.x < (_screenBounds.minX + _fadeOffset) && !isFadingOut)
        {
            isFadingOut = true;
            StartCoroutine(FadeOutDestroy());
        }

        // to the right
        if (speed < 0 && transform.position.x > (_screenBounds.minX - _fadeOffset) && !isFadingOut)
        {
            isFadingOut = true;
            StartCoroutine(FadeOutDestroy());
        }

        if (_objectScript.drag == true && !isFadingOut && RectTransformUtility.RectangleContainsScreenPoint(_transform, Input.mousePosition, Camera.main))
        {
            Debug.Log("hit");
            // ...
        }
    }
    private IEnumerator FadeIn()
    {
        float a = 0f;
        while (a < fadeDuration)
        {
            a += Time.deltaTime;
            _group.alpha = Mathf.Lerp(0f, 1f, a / fadeDuration);
            yield return null;
        }
        _group.alpha = 1f;
    }
    private IEnumerator FadeOutDestroy()
    {
        float a = 0f;
        float alpha = _group.alpha;
        while (a < fadeDuration)
        {
            a += Time.deltaTime;
            _group.alpha = Mathf.Lerp(alpha, 0f, a / fadeDuration);
            yield return null;
        }
        _group.alpha = 0f;
        Destroy(gameObject);
    }
    private IEnumerator ShrinkAndDestroy(GameObject target, float duration)
    {
        Vector3 orgScale = target.transform.localScale;
        Quaternion orgRot = target.transform.rotation;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            target.transform.localScale = Vector3.Lerp(orgScale, Vector3.zero, t / duration);
            float angle = Mathf.Lerp(0, 360, t / duration);
            target.transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
        // next actions
        Destroy(target);
    }
    private IEnumerator RecoverColour ()
    {
        yield return new WaitForSeconds(0.5f);
        _image.color = _orgColor;
    }
    private IEnumerator Vibrate()
    {
        Vector2 orgPos = _transform.anchoredPosition;
        float dur = 0.3f;
        float elapsed = 0f;
        float intensity = 5f;
        while (elapsed < dur)
        {
            _transform.anchoredPosition = orgPos + Random.insideUnitCircle * intensity;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
