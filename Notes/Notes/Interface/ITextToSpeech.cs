using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Interface
{
    // интерфейс для синтеза речи.
    public interface ITextToSpeech
    {
        void Speak(string text); // для озвучивания текста.
    }
}
