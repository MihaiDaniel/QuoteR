using Quoter.Framework.Enums;

namespace Quoter.Framework.Services
{
	public interface ISoundService
	{
		void LoadSoundsAsync();
		void PlayNotificationSound();
		void PlayWarningSound();
		void Play(EnumSound sound);
	}
}
