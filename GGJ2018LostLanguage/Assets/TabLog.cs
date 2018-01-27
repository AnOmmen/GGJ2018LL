using System.Collections.Generic;

public class TabLog {

    public readonly List<Association> associations;

    public void LogAssociation(Association association)
    {
        associations.Add(association);
    }

    public Association RemoveAssociationAt(int index)
    {
        Association removed_association = associations[index];
        associations.RemoveAt(index);
        return removed_association;
    }
	
}
