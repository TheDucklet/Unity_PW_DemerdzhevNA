using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Анимация 3D-объектов и UI-кнопок с правильной обработкой звука клика
/// </summary>
public class ProceduralAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки 3D Анимации")]
    [SerializeField] private bool animate3D = false;
    [SerializeField] private float floatAmplitude = 0.15f;
    [SerializeField] private float floatSpeed = 3.0f;
    [SerializeField] private float rotateSpeed = 90.0f;

    [Header("Настройки UI Анимации")]
    [SerializeField] private bool animateUI = false;
    [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float scaleSpeed = 10.0f;

    private Vector3 startPosition;
    private Vector3 defaultScale;
    private Vector3 targetScale;

    private void Start()
    {
        startPosition = transform.position;
        defaultScale = transform.localScale;
        targetScale = defaultScale;

        // Автоматически привязываем звук клика к нажатию на UI кнопку
        if (animateUI)
        {
            Button btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    if (AudioManager.Instance != null)
                        AudioManager.Instance.PlayButtonClickSound();
                });
            }
        }
    }

    private void Update()
    {
        if (animate3D)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        }

        if (animateUI)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animateUI)
        {
            targetScale = Vector3.Scale(defaultScale, hoverScale);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animateUI)
        {
            targetScale = defaultScale;
        }
    }
}