using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public class DirectoryReader : IDirectoryReader
	{
		private readonly object _lock = new object();

		private bool __isScanInProgress = false;
		private bool _isScanInProgress
		{
			get
			{
				lock (_lock)
				{
					return __isScanInProgress;
				}
			}
			set
			{
				lock (_lock)
				{
					__isScanInProgress = value;
				}
			}
		}

		private readonly IFileReader _fileReader;
		private readonly IMemoryCache _memoryCache;

		public DirectoryReader(IFileReader fileReader, IMemoryCache memoryCache)
		{
			_fileReader = fileReader;
			_memoryCache = memoryCache;
		}

		public async Task<List<FileModel>> ScanDirectoryAsync(string dirPath)
		{
			if (!Directory.Exists(dirPath))
			{
				throw new ArgumentException($"Directory does not exist: {dirPath}");
			}

			List<FileModel> lstFiles = new();
			if (_isScanInProgress)
			{
				return lstFiles;
			}
			try
			{
				_isScanInProgress = true;
				string[] arrFileNames = Directory.GetFiles(dirPath);

				foreach (string fileName in arrFileNames)
				{
					string filePath = Path.Combine(dirPath, fileName);
					if (CanReadFile(filePath))
					{
						FileModel fileModel = await ReadFileAsync(filePath);
						lstFiles.Add(fileModel);
					}
				}
			}
			finally
			{
				_isScanInProgress = false;
			}
			return lstFiles;
		}

		private bool CanReadFile(string filePath)
		{
			DateTime lastWriteDateTime = File.GetLastWriteTime(filePath);
			DateTime? lastWriteDateTimeInMemCache = _memoryCache.GetOrDefault<DateTime?>(filePath);

			if (lastWriteDateTimeInMemCache == default)
			{
				_memoryCache.TryAdd(filePath, lastWriteDateTime);
				return true;
			}
			else if(lastWriteDateTime > lastWriteDateTimeInMemCache)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private async Task<FileModel> ReadFileAsync(string filePath)
		{
			List<string> lstFileLines = await _fileReader.ReadAsync(filePath);
			return new FileModel()
			{
				Name = Path.GetFileName(filePath),
				NameWithNoExtension = Path.GetFileNameWithoutExtension(filePath),
				Path = filePath,
				LstLines= lstFileLines
			};
		}
	}
}
