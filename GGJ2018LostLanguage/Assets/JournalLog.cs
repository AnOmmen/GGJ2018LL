public class JournalLog {

    public enum TabID
    {
        CORRECT = 0,
        UNKNOWN,
        INCORRECT,
        NUMBER_OF_TABS
    }

    TabLog[] tabs;

    public JournalLog()
    {
        tabs = new TabLog[] { new TabLog(), new TabLog(), new TabLog() };
    }

    public TabLog GetTab(TabID tab_id)
    {
        return tabs[(int)tab_id];
    }

}
