public class Association {

    public readonly bool valid;

    public readonly LanguageLibrary.WordID icon_word_id;
    public readonly LanguageLibrary.WordID symbol_word_id;

    public Association(LanguageLibrary.WordID _icon_word_id, LanguageLibrary.WordID _symbol_word_id)
    {
        icon_word_id = _icon_word_id;
        symbol_word_id = _symbol_word_id;
        valid = icon_word_id == symbol_word_id;
    }

}
