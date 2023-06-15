/*using SpeechTts = Xamarin.Forms.PlatformConfiguration.Android.Speech.Tts;
using Xamarin.Forms;
using Notes.Interface;

[assembly: Dependency(typeof(ITextToSpeech))]
namespace TextToSpeech.Droid
{
    public class TextToSpeech : Java.Lang.Object, ITextToSpeech, SpeechTts.TextToSpeech.IOnInitListener
    {
        SpeechTts.TextToSpeech speaker;
        string _text;

        public void Speak(string text)
        {
            _text = text;
            if (!this.IsSpeakerInitialized(speaker))
            {
                speaker = this.InitializingSpeaker();
            }

            speaker.Speak(_text, SpeechTts.QueueMode.Flush, null, null);

        }

        public void OnInit(SpeechTts.OperationResult operationResult)
        {
            if (operationResult.Equals(SpeechTts.OperationResult.Success))
            {
                speaker.Speak(_text, SpeechTts.QueueMode.Flush, null, null);
            }
        }

        private bool IsSpeakerInitialized(SpeechTts.TextToSpeech _speaker)
        {
            if (_speaker != null)
            {
                return true;
            }

            return false;
        }

        private SpeechTts.TextToSpeech InitializingSpeaker()
        {
            return new SpeechTts.TextToSpeech(MainActivity.Instance, this);
        }
    }
}
*/