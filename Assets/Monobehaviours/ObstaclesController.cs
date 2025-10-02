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

    private bool isExploding = false;


    public ObstacleSpawner _spawner;
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
        _spawner = Object.FindFirstObjectByType<ObstacleSpawner>();

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
        if (speed < 0 && transform.position.x > (_screenBounds.maxX - _fadeOffset) && !isFadingOut)
        {
            isFadingOut = true;
            StartCoroutine(FadeOutDestroy());
        }
        if (tag == "bomb" && !isExploding && RectTransformUtility.RectangleContainsScreenPoint(_transform, Input.mousePosition, Camera.main))
        {
            Animator bombAnimator = GetComponent<Animator>();
            if (bombAnimator != null)
            {
                isExploding = true;
                bombAnimator.SetBool("explosion", true);
                if (_spawner._audio != null) StartCoroutine(PlayOneShotDelayed(0.58f, 1, 5f));
                Explosion();
                if (_objectScript.drag == true)
                {
                    if (_objectScript.lastDragged != null)
                    {
                        StartCoroutine(ShrinkAndDestroy(_objectScript.lastDragged, 0.5f));
                        _objectScript.lastDragged = null;
                        _objectScript.drag = false;
                    }
                }
            }
            
        }

        if (_objectScript.drag == true && !isFadingOut && RectTransformUtility.RectangleContainsScreenPoint(_transform, Input.mousePosition, Camera.main) && tag != "bomb")
        {
            Debug.Log("car hit! ahhh!!!");
            if (_objectScript.lastDragged != null)
            {
                StartCoroutine(ShrinkAndDestroy(_objectScript.lastDragged, 0.5f));
                _objectScript.lastDragged = null;
                _objectScript.drag = false;
            }
            StartCoroutine(FadeOutDestroy());
            isFadingOut = true;
            _image.color = Color.magenta;
            StartCoroutine(RecoverColour(0.5f));
            StartCoroutine(Vibrate());
            if (_spawner._audio != null) _spawner._audio.PlayOneShot(_spawner._effects[0]);
            _objectScript.vehiclesDestroyed++;
            _objectScript.vehiclesRemaining--;
            Debug.Log("vehicles remaining" + _objectScript.vehiclesRemaining);
        }
    }
    private void Explosion()
    {
        //_image.color = Color.red; later
        StartCoroutine(RecoverColour(0.3f));
        StartCoroutine(Vibrate());
        StartCoroutine(WaitBeforeExplode());
    }
    private IEnumerator WaitBeforeExplode()
    {
        float radius = 0;
        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D col)) {
            radius = col.radius * transform.lossyScale.x;
            yield return new WaitForSeconds(0.65f);
            ExplodeObjects(radius);
            yield return new WaitForSeconds(0.45f);
            StartCoroutine(FadeOutDestroy());
        }
 
    }
    private void ExplodeObjects(float rad)
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, rad);
        foreach (Collider2D item in hit)
        {
            if (item != null && item.gameObject != gameObject)
            {
                ObstaclesController obj = item.GetComponent<ObstaclesController>();
                if (obj != null && !obj.isExploding)
                {
                    obj.StartToDestroy();
                }
            }
        }
    }
    public void StartToDestroy()
    {
        StartCoroutine(FadeOutDestroy());
        isFadingOut = true;
        _image.color = Color.cyan;
        StartCoroutine(RecoverColour(0.5f));
        StartCoroutine(Vibrate());
        if (_spawner._audio != null) _spawner._audio.PlayOneShot(_spawner._effects[0]);
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
    private IEnumerator RecoverColour (float sec)
    {
        yield return new WaitForSeconds(sec);
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
    private IEnumerator PlayOneShotDelayed(float sec, int idx, float vol)
    {
        yield return new WaitForSeconds(sec);
        _spawner._audio.PlayOneShot(_spawner._effects[idx], vol);
    }
}
