using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Color active;
    public Color normal;
    public ButtonType type;

    public enum ButtonType
    {
        NEW, CREDITS, QUIT, RETURN
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Text>().color = active;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Text>().color = normal;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (type)
        {
            case ButtonType.NEW:
                Debug.Log("NEW GAME BUTTON PRESSED");
                SceneManager.LoadScene("HelpControls");
                break;
            case ButtonType.CREDITS:
                Debug.Log("CREDITS BUTTON PRESSED");
                SceneManager.LoadScene("Credits");
                break;
            case ButtonType.RETURN:
                Debug.Log("Return BUTTON PRESSED");
                SceneManager.LoadScene("Start Menu");
                break;
            case ButtonType.QUIT:
                UnityEditor.EditorApplication.isPlaying = false;
                break;
        }
    }
}
