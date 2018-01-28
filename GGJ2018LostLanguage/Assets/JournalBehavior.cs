using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalBehavior : MonoBehaviour {

    public JournalManager journal_manager;

    UnityEngine.Events.UnityAction on_show_hide_journal_click;
    UnityEngine.Events.UnityAction on_open_journal_cover_click;
    UnityEngine.Events.UnityAction on_turn_page_left_click;
    UnityEngine.Events.UnityAction on_turn_page_right_click;

    Transform show_hide_journal;
    Transform journal_cover;
    Transform journal_interior;
    Transform turn_page_right;
    Transform open_anchor;
    Transform closed_anchor;
    Transform content;
    float entry_left_anchor_height;
    float entry_right_anchor_height;

    [SerializeField]
    GameObject entry_left_anchor;
    [SerializeField]
    GameObject entry_right_anchor;

    [SerializeField]
    float toggle_speed;

    LanguageLibrary language_library;

    UnityEngine.Events.UnityAction on_association_created;

    enum JournalToggleState
    {
        OPEN = 0,
        CLOSED,
        NUMBER_OF_TOGGLE_STATES
    }

    [SerializeField]
    JournalToggleState journal_toggle_state;
    float travel_distance;

    enum JournalPageState
    {
        COVER = 0,
        CORRECT,
        UNKNOWN,
        INCORRECT,
        NUMBER_OF_PAGE_STATES
    }

    [SerializeField]
    JournalPageState journal_page_state;

    void Awake()
    {
        journal_manager = new JournalManager(new List<Association>());
    }

    void Start()
    {
        travel_distance = 0f;

        on_association_created = new UnityEngine.Events.UnityAction(SetPageParts);
        journal_manager.on_association_created.AddListener(on_association_created);
        language_library = GameObject.Find("LanguageLibrary").GetComponent<LanguageLibrary>();

        open_anchor = this.transform.parent.Find("JournalOpenAnchor");
        closed_anchor = this.transform.parent.Find("JournalClosedAnchor");
        journal_cover = this.transform.Find("JournalCover");
        journal_interior = this.transform.Find("JournalInterior");
        content = journal_interior.Find("Scroll View").Find("Viewport").Find("Content");
        entry_left_anchor_height = entry_left_anchor.GetComponent<RectTransform>().rect.height;
        entry_right_anchor_height = entry_right_anchor.GetComponent<RectTransform>().rect.height;

        on_open_journal_cover_click = new UnityEngine.Events.UnityAction(OnOpenJournalCoverClick);
        journal_cover.Find("OpenJournalCover").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(on_open_journal_cover_click);

        on_turn_page_left_click = new UnityEngine.Events.UnityAction(OnTurnPageLeftClick);
        journal_interior.Find("TurnPageLeft").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(on_turn_page_left_click);

        on_turn_page_right_click = new UnityEngine.Events.UnityAction(OnTurnPageRightClick);
        turn_page_right = journal_interior.Find("TurnPageRight");
        turn_page_right.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(on_turn_page_right_click);

        on_show_hide_journal_click = new UnityEngine.Events.UnityAction(OnShowHideJournalClick);
        show_hide_journal = this.transform.Find("ShowHideJournal");
        show_hide_journal.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(on_show_hide_journal_click);

        if (journal_toggle_state == JournalToggleState.OPEN)
        {
            this.transform.position = open_anchor.position;
        }
        else
        {
            this.transform.position = closed_anchor.position;
        }

        SetPageParts();
    }

    void Update()
    {
        if (travel_distance != 0f)
        {
            MoveJournal();
        }
    }

    void OnShowHideJournalClick()
    {
        travel_distance = open_anchor.position.y - closed_anchor.position.y;
        show_hide_journal.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    void OnOpenJournalCoverClick()
    {
        ChangePage(true);
    }

    void OnTurnPageLeftClick()
    {
        ChangePage(false);
    }

    void OnTurnPageRightClick()
    {
        ChangePage(true);
    }

    void MoveJournal()
    {
        float move_step = toggle_speed * Time.deltaTime;
        if (travel_distance - move_step < 0f)
        {
            move_step = travel_distance;
            travel_distance = 0f;
        }
        else
        {
            travel_distance -= move_step;
        }
        
        if (journal_toggle_state == JournalToggleState.OPEN)
        {
            this.transform.Translate(0f, move_step * -1f, 0f);
            CheckStateToggle();
        }
        else
        {
            this.transform.Translate(0f, move_step, 0f);
            CheckStateToggle();
        }
    }

    void CheckStateToggle()
    {
        if (travel_distance == 0f)
        {
            if (journal_toggle_state == JournalToggleState.OPEN)
            {
                journal_toggle_state = JournalToggleState.CLOSED;
            }
            else
            {
                journal_toggle_state = JournalToggleState.OPEN;
            }
            show_hide_journal.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
    }

    void ChangePage(bool turn_right)
    {
        switch (journal_page_state)
        {
            case JournalPageState.COVER:
                if (turn_right)
                {
                    journal_page_state = JournalPageState.CORRECT;
                }
                else
                {
                    return;
                }
                break;
            case JournalPageState.CORRECT:
                if (turn_right)
                {
                    journal_page_state = JournalPageState.UNKNOWN;
                }
                else
                {
                    journal_page_state = JournalPageState.COVER;
                }
                break;
            case JournalPageState.UNKNOWN:
                if (turn_right)
                {
                    journal_page_state = JournalPageState.INCORRECT;
                }
                else
                {
                    journal_page_state = JournalPageState.CORRECT;
                }
                break;
            case JournalPageState.INCORRECT:
                if (turn_right)
                {
                    return;
                }
                else
                {
                    journal_page_state = JournalPageState.UNKNOWN;
                }
                break;
        }
        SetPageParts();
    }

    void SetPageParts()
    {
        switch (journal_page_state)
        {
            case JournalPageState.COVER:
                journal_cover.gameObject.SetActive(true);
                journal_interior.gameObject.SetActive(false);
                break;
            case JournalPageState.CORRECT:
                journal_cover.gameObject.SetActive(false);
                journal_interior.gameObject.SetActive(true);
                LoadPageAssociations(JournalLog.TabID.CORRECT);
                break;
            case JournalPageState.UNKNOWN:
                journal_cover.gameObject.SetActive(false);
                turn_page_right.gameObject.SetActive(true);
                LoadPageAssociations(JournalLog.TabID.UNKNOWN);
                break;
            case JournalPageState.INCORRECT:
                journal_cover.gameObject.SetActive(false);
                turn_page_right.gameObject.SetActive(false);
                LoadPageAssociations(JournalLog.TabID.INCORRECT);
                break;
        }
    }

    void LoadPageAssociations(JournalLog.TabID tab_id)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        List<Association> associations = journal_manager.GetAssociations(tab_id);
        GameObject entry;
        int offset = 0;
        for (int i = 0; i < associations.Count; ++i)
        {
            if (i % 2 == 0)
            {
                entry = Instantiate(entry_left_anchor, content);
                entry.transform.Translate(new Vector3(0f, entry_left_anchor_height * offset, 0f));
                
            }
            else
            {
                entry = Instantiate(entry_right_anchor, content);
                entry.transform.Translate(new Vector3(0f, entry_right_anchor_height * offset, 0f));
                offset -= 1;
            }
            entry.transform.Find("IconPentagon").Find("IconDisplay").GetComponent<UnityEngine.UI.Image>().sprite = language_library.GetIcon(associations[i].icon_word_id);
            entry.transform.Find("SymbolPentagon").Find("SymbolDisplay").GetComponent<UnityEngine.UI.Image>().sprite = language_library.GetSymbol(associations[i].symbol_word_id);
        }
    }

}
