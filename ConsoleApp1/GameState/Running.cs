using System;

public class Running
{
    SoundsPlayer soundsPlayer;
    string music = "assets/Music.wav";
    public Running(SoundsPlayer soundsPlayer)
    {
        this.soundsPlayer = soundsPlayer;
        soundsPlayer.PlaySoundAsync(music, true);
    }
}
