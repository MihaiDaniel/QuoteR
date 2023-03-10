using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public class SoundService : ISoundService
	{
		SoundPlayer _soundPlayer;

		public SoundService()
		{
			_soundPlayer = new SoundPlayer();
		}

		public Task LoadAsync(string filePath)
		{
			throw new NotImplementedException();
		}

		public void Play(EnumSound sound)
		{
			throw new NotImplementedException();
		}
	}
}
