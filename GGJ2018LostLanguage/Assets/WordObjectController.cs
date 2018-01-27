using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObjectController : MonoBehaviour {
    
    public readonly LanguageLibrary.WordID word_id;

    public class OnWordObjectMouseDownEvent : UnityEngine.Events.UnityEvent<WordObjectController> { }

    public OnWordObjectMouseDownEvent on_mouse_down;

    void Start()
    {
        on_mouse_down = new OnWordObjectMouseDownEvent();
    }

    void OnMouseDown()
    {
        on_mouse_down.Invoke(this);
    }
}
