using System.Xml;
using NAudio.Wave;
public class SoundsPlayer
{
    private AudioFileReader audioFile; // Store the audio file at class level
    private WaveOutEvent outputDevice; // Store the output device at class level
    public async Task PlaySoundAsync(string soundFilePath, bool loop)
    {
        await Task.Run(() =>
        {
            try
            {
                audioFile = new AudioFileReader(soundFilePath);
                using (var outputDevice = new WaveOutEvent())
                {
                    audioFile.Volume = 0.05f;
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    if (loop == true){
                        outputDevice.PlaybackStopped += (sender, args) =>
                        {
                            // Reset the position of the audio file and play again
                            audioFile.Position = 0;
                            outputDevice.Play();
                        };
                    }

                    // Wait till the audio is playing
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if any
                Console.WriteLine("Error playing sound: " + ex.Message);
            }
        });
    }
}
