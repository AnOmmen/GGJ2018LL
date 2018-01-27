using System.Collections.Generic;

public class LanguageLibrary
{
    public enum WordID
    {
        NUMBER_OF_WORDS
    }


    public struct Word
    {
        UnityEngine.UI.Image unknown_symbol;
        UnityEngine.UI.Image known_symbol;

        string meaning;

        //AudioClip sound { get; set; }
    }

    Dictionary<WordID, Word> word_dictionary;

    public LanguageLibrary()
    {
        word_dictionary = new Dictionary<WordID, Word>();
    }

    public Word GetWord(WordID word_id)
    {
        return word_dictionary[word_id];
    }

    public void SetWord(WordID word_id, Word word)
    {
        word_dictionary[word_id] = word;
    }

}
