using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObjectController : MonoBehaviour {
    
    public enum WordObjectID
    {
        ICON = 0,
        SYMBOL,
        NUMBER_OF_IDS
    }

    [SerializeField]
    public LanguageLibrary.WordID word_id;

    [SerializeField]
    public WordObjectID type_id;

    [SerializeField]
    float scale_size;

    public class OnWordObjectMouseDownEvent : UnityEngine.Events.UnityEvent<WordObjectController> { }

    public OnWordObjectMouseDownEvent on_mouse_down;

    LanguageLibrary language_library;

    SpriteRenderer sprite_renderer;

    bool highlighted;

    void Awake()
    {
        on_mouse_down = new OnWordObjectMouseDownEvent();
        highlighted = false;
    }

    void Start()
    {
        language_library = GameObject.Find("LanguageLibrary").GetComponent<LanguageLibrary>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        
        if (type_id == WordObjectID.SYMBOL)
        {
            sprite_renderer.sprite = language_library.GetSymbol(word_id);
        }
        else
        {
            sprite_renderer.sprite = language_library.GetIcon(word_id);
        }

        sprite_renderer.transform.localScale = sprite_renderer.sprite.bounds.size * scale_size;
    }

    void OnMouseDown()
    {
        on_mouse_down.Invoke(this);
        ToggleHighlight();
    }
    
    public void ToggleHighlight()
    {
        if (!highlighted)
        {
            sprite_renderer.color = new Color(255, 237, 0, 255);
        }
        else
        {
            sprite_renderer.color = new Color(255, 255, 255, 255);
        }
        highlighted = !highlighted;
    }

}
