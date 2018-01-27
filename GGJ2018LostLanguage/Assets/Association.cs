public class Association {

    public readonly bool valid;

    public readonly LanguageLibrary.WordID word_id;

	public Association(LanguageLibrary.WordID id_x, LanguageLibrary.WordID id_y)
    {
        if (id_x == id_y)
        {
            valid = true;
            word_id = id_x;
        }
        else
        {
            valid = false;
        }
    }

}
