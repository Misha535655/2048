using UnityEngine;
using Zenject;

public class SwipeDetector : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 detection);

    [SerializeField] private float deadZone = 80f;

    private Vector2 tapPosition;
    private Vector2 swipeDelta;

    private bool isSwiping;
    private bool isMobile;

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (!isMobile)
        {
            HandleMouseInput();
        }
        else
        {
            HandleTouchInput();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            tapPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ResetSwipe();
        }
        else if (isSwiping)
        {
            swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            CheckSwipe();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwiping = true;
                    tapPosition = touch.position;
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    ResetSwipe();
                    break;
            }

            if (isSwiping)
            {
                swipeDelta = touch.position - tapPosition;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        if (swipeDelta.magnitude > deadZone)
        {
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    SwipeEvent(swipeDelta.x > 0 ? Vector2.right : Vector2.left);
                }
                else
                {
                    SwipeEvent(swipeDelta.y > 0 ? Vector2.up : Vector2.down);
                }
                ResetSwipe();
            }
        }
    }

    private void ResetSwipe()
    {
        isSwiping = false;
        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
}