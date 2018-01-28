using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalBehavior : MonoBehaviour {

    JournalManager journal_manager;
    
    UnityEngine.Events.UnityAction on_show_hide_journal_click;

    Transform show_hide_journal_button;

    [SerializeField]
    float open_y;
    [SerializeField]
    float closed_y;
    [SerializeField]
    float toggle_speed;

    enum JournalState
    {
        OPEN = 0,
        CLOSED,
        NUMBER_OF_TOGGLE_STATES
    }

    [SerializeField]
    JournalState journal_state = JournalState.CLOSED;
    float toggle_distance;

    void Start()
    {
        toggle_distance = 0f;
        on_show_hide_journal_click = new UnityEngine.Events.UnityAction(OnShowHideJournalClick);
        show_hide_journal_button = transform.Find("ShowHideJournal");
        show_hide_journal_button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(on_show_hide_journal_click);
    }

    void Update()
    {
        if (toggle_distance != 0f)
        {
            if (journal_state == JournalState.OPEN)
            {
                
            }
        }
    }

    void OnShowHideJournalClick()
    {
        if (journal_state == JournalState.CLOSED)
        {
            toggle_distance = closed_y - open_y;
        }
        else
        {
            toggle_distance = open_y - closed_y;
        }
        show_hide_journal_button.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

}
