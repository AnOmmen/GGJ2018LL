using System.Collections.Generic;

public class JournalManager {

    JournalLog journal_log;

    public UnityEngine.Events.UnityEvent on_association_created;

    public JournalManager(List<Association> beginning_associations)
    {
        journal_log = new JournalLog();
        on_association_created = new UnityEngine.Events.UnityEvent();

        foreach (Association association in beginning_associations)
        {
            CreateAssociation(association, JournalLog.TabID.CORRECT);
        }
    }

    public void CreateAssociation(Association association, JournalLog.TabID tab_id = JournalLog.TabID.UNKNOWN)
    {
        journal_log.GetTab(tab_id).LogAssociation(association);
        on_association_created.Invoke();
    }

    public List<Association> GetAssociations(JournalLog.TabID tab_id)
    {
        return journal_log.GetTab(tab_id).associations;
    }

    public bool VerifyAssociation(int index)
    {
        if (journal_log.GetTab(JournalLog.TabID.UNKNOWN).associations[index].valid)
        {
            journal_log.GetTab(JournalLog.TabID.CORRECT).LogAssociation(journal_log.GetTab(JournalLog.TabID.UNKNOWN).RemoveAssociationAt(index));
            return true;
        }
        else
        {
            journal_log.GetTab(JournalLog.TabID.INCORRECT).LogAssociation(journal_log.GetTab(JournalLog.TabID.UNKNOWN).RemoveAssociationAt(index));
            return false;
        }
    }
    
}
