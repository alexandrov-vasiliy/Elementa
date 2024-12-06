using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystick;
    [SerializeField] private Image _joystickArea;

    private Vector2 _joystickBackgroundStartPosition;

    protected Vector2 _inputVector;

    public Color inActiveJoystickColor;
    public Color activeJoystickColor;

    private bool _joystickIsActive = false;

    private void Start()
    {
        ClickEffect();

        _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, eventData.pressEventCamera, out joystickPosition))
        {
            joystickPosition.x = (joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x);
            joystickPosition.y = (joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();

        Vector2 joystickBackgroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, eventData.pressEventCamera, out joystickBackgroundPosition))
        {
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(joystickBackgroundPosition.x, joystickBackgroundPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;

        ClickEffect();

        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void ClickEffect()
    {
        if (!_joystickIsActive)
        {
            _joystick.color = inActiveJoystickColor;
            _joystickIsActive = true;
        }
        else
        {
            _joystick.color = activeJoystickColor;
            _joystickIsActive = false;
        }
    }
}
