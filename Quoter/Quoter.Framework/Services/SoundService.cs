using Quoter.Framework.Enums;
using Quoter.Framework.Services.AppSettings;
using System.Media;
using System.Reflection;

namespace Quoter.Framework.Services
{
    public class SoundService : ISoundService
	{
		private readonly Dictionary<EnumSound, SoundPlayer> _dicSoundPlayers;
		private readonly Assembly _entryAssembly;
		private readonly IAppSettings _settings;
		private bool _isLoaded;

		public SoundService(IAppSettings settings)
		{
			_dicSoundPlayers = new Dictionary<EnumSound, SoundPlayer>();
			_entryAssembly = Assembly.GetEntryAssembly();
			_settings = settings;
			_isLoaded = false;
		}

		public void LoadSoundsAsync()
		{
			LoadSound(EnumSound.Click, "Quoter.App.Resources.snd-intuition-561.wav");
			LoadSound(EnumSound.Pop, "Quoter.App.Resources.snd-all-eyes-on-me-465.wav");
			LoadSound(EnumSound.Arpeggio, "Quoter.App.Resources.snd-arpeggio-467.wav");
			LoadSound(EnumSound.Bell, "Quoter.App.Resources.snd-appointed-529.wav");
			_isLoaded = true;
		}

		public void PlayNotificationSound()
		{
			Play(_settings.NotificationSound);
		}

		public void PlayWarningSound()
		{
			SystemSounds.Asterisk.Play();
		}

		public void Play(EnumSound sound)
		{
			if(!_isLoaded)
			{
				throw new InvalidOperationException($"{nameof(LoadSoundsAsync)} must be called before attempting to play!");
			}
			_dicSoundPlayers.TryGetValue(sound, out SoundPlayer player);
			if (player != null && player.IsLoadCompleted)
			{
				player.Play();
			}
		}

		private void LoadSound(EnumSound enumSound, string soundResourceName)
		{
			Stream stream = _entryAssembly.GetManifestResourceStream(soundResourceName);
			SoundPlayer player = new SoundPlayer(stream);
			player.LoadAsync();
			_dicSoundPlayers.Add(enumSound, player);
		}
	}
}
