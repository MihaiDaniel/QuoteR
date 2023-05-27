using Quoter.Shared.Enums;

namespace Quoter.Shared.Models
{
	public class Version
	{
		public int Major { get; private set; }

		public int Minor { get; private set; }

		public int Build { get; private set; }

		public int Revision { get; private set; }

		public Version(string version)
		{
			string[] component = version.Split('.');
			Major = int.Parse(component[0]);
			Minor = int.Parse(component[1]);
			Build = int.Parse(component[2]);
			Revision = int.Parse(component[3]);
		}

		/// <summary>
		/// Compares the current version with <paramref name="version"/>. 
		/// Returns a value indicating how this version is in comparison.
		/// Is it Older/Newer/Equal in regards to the one to compare 
		/// </summary>
		public EnumVersionCompare CompareWith(Version version)
		{
			if(Major == version.Major 
				&& Minor == version.Minor 
				&& Build == version.Build 
				&& Revision == version.Revision)
			{
				return EnumVersionCompare.Equal;
			}
			if(Major > version.Major)
			{
				return EnumVersionCompare.Newer;
			}
			else if (Major == version.Major)
			{
				if(Minor > version.Minor)
				{
					return EnumVersionCompare.Newer;
				}
				else if (Minor == version.Minor)
				{
					if(Build > version.Build)
					{
						return EnumVersionCompare.Newer;
					}
					else if(Build == version.Build && Revision > version.Revision)
					{
						return EnumVersionCompare.Newer;
					}
				}
			}
			return EnumVersionCompare.Older;
		}
	}
}
