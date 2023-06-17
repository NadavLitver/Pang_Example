namespace view
{
    public interface ISoundManager
    {
        void Play(SoundManager.Sound sound);
        void Play(SoundManager.Sound sound, float volume);
        void Play(SoundManager.Sound sound, float volume, float pitch);
    }
}