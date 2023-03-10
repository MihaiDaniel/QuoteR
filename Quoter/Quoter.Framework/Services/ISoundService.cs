using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public interface ISoundService
	{
		Task LoadAsync(string filePath);

		void Play(EnumSound sound);
	}
}
