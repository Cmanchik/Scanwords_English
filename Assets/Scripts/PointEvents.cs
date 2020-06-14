using UnityEngine.EventSystems;
using UnityEngine;

public class PointEvents : MonoBehaviour
{
    public void OnMouseDown()
    {
        InputManager.Instance.StartSelectLetters(gameObject);
    }

    public void OnMouseEnter()
    {
        InputManager.Instance.SelectLetter(gameObject);
    }

    public void OnMouseUp()
    {
        InputManager.Instance.InputLetters();
    }
}
