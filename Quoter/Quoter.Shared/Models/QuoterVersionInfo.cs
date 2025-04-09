using Quoter.Shared.Enums;
using System;

namespace Quoter.Shared.Models
{
	/// <summary>
	/// This helper class holds version information of the QuoterApplication
	/// </summary>
	/// <remarks>
	/// Id, Major, Minor, Build, Revision
	/// </remarks>
	public class QuoterVersionInfo
	{
		/// <summary>
		/// Unique id of the version as is on the update server.
		/// </summary>
		public string PublicId { get; set; }

		public int Major { get; private set; }

		public int Minor { get; private set; }

		public int Build { get; private set; }

		public int Revision { get; private set; }

		public QuoterVersionInfo(string id, string version)
		{
			PublicId = id;
			string[] component = version.Split('.');
			Major = int.Parse(component[0]);
			Minor = int.Parse(component[1]);
			Build = int.Parse(component[2]);
			Revision = int.Parse(component[3]);
		}

		public QuoterVersionInfo(string version)
		{
			string[] component = version.Split('.');
			Major = int.Parse(component[0]);
			Minor = int.Parse(component[1]);
			Build = int.Parse(component[2]);
			Revision = int.Parse(component[3]);
		}

		public QuoterVersionInfo(int major, int minor, int build, int revision)
		{
			Major = major;
			Minor = minor;
			Build = build;
			Revision = revision;
		}

		/// <summary>
		/// Verifies if this version is older than the <paramref name="other"/> version specified
		/// </summary>
		/// <returns>True if this is older, false if equal or this is newer</returns>
		public bool IsOlderThan(QuoterVersionInfo other)
		{
			return this.CompareWith(other) == EnumVersionCompare.Older;
		}

		/// <summary>
		/// Compares the current version with <paramref name="version"/>. 
		/// Returns a value indicating how this version is in comparison.
		/// Is it Older/Newer/Equal in regards to the one to compare 
		/// </summary>
		/// <remarks>
		/// If this version is newer than <paramref name="version"/> it returns <see cref="EnumVersionCompare.Newer"/>
		/// If this version is older than <paramref name="version"/> it returns <see cref="EnumVersionCompare.Older"/>
		/// If they are equal it returns <see cref="EnumVersionCompare.Equal"/>
		/// </remarks>
		public EnumVersionCompare CompareWith(QuoterVersionInfo version)
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

		/// <summary>
		/// Returns the version as Major.Minor.Build.Revision
		/// </summary>
		public override string ToString()
		{
			return $"{Major}.{Minor}.{Build}.{Revision}";
		}
	}
}
