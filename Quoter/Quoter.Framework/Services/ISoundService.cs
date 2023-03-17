using Quoter.Framework.Enums;

namespace Quoter.Framework.Services
{
	public interface ISoundService
	{
		void LoadSoundsAsync();
		void Play(EnumSound sound);
	}
}
