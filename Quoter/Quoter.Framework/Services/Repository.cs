﻿using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Quoter.Framework.Services
{
	public class Repository : IRepository
	{
		public Task<BookModel> GetBookAsync(string name)
		{
			BookModel book = new BookModel()
			{
				Name = name,
				LstChapters = new List<ChapterModel>()
				{
					new ChapterModel()
					{
						Name= name,
						LstContent = new List<string>()
						{
							"ed version of the accepted answer that does NOT require you to type names of properties manually in every pro",
							"anged(\"some-property-name\"). Instead you just call OnPropertyChanged() withou"
						}
					}
				}
			};
			return Task.FromResult(book);
		}

		public Task<List<BookModel>> GetBooksAsync(string collectionName)
		{
			List<BookModel> bookModels = new List<BookModel>();

			if (collectionName == "My collection one")
			{
				bookModels.Add(new BookModel()
				{
					Name = "Harry potter and the prisoner",
					LstChapters = new List<ChapterModel>()
					{
						new ChapterModel()
						{
							Name= "A new prisoner",
							LstContent = new List<string>()
							{
								"ed version of the accepted answer that does NOT require you to type names of properties manually in every pro",
								"anged(\"some-property-name\"). Instead you just call OnPropertyChanged() withou"
							}
						},
						new ChapterModel()
						{
							Name= "Going to castle",
							LstContent = new List<string>()
							{
								"ed version of the accepted answer that does NOT require you to type names of properties manually in every pro",
								"anged(\"some-property-name\"). Instead you just call OnPropertyChanged() withou"
							}
						}
					},
				});
				bookModels.Add(new BookModel()
				{
					Name = "Harry potter and the something stone",
					LstChapters = new List<ChapterModel>()
					{
						new ChapterModel()
						{
							Name= "The stone cold",
							LstContent = new List<string>()
							{
								"ed version of the accepted answer that does NOT require you to type names of properties manually in every pro",
								"anged(\"some-property-name\"). Instead you just call OnPropertyChanged() withou"
							}
						}
					},
				});
			}
			else if (collectionName == "My collection two")
			{
				bookModels.Add(new BookModel()
				{
					Name = "Discovey channel",
					LstChapters = new List<ChapterModel>()
					{
						new ChapterModel()
						{
							Name= "The new discovery",
							LstContent = new List<string>()
							{
								"ed version of the accepted answer that does NOT require you to type names of properties manually in every pro",
								"anged(\"some-property-name\"). Instead you just call OnPropertyChanged() withou"
							}
						},
						new ChapterModel()
						{
							Name= "Discovery two",
							LstContent = new List<string>()
							{
								"ed version of the accepted answer that does NOT require you to type names of properties manually in every pro",
								"anged(\"some-property-name\"). Instead you just call OnPropertyChanged() withou"
							}
						}
					},
				});
			}

			return Task.FromResult(bookModels);
		}

		public async Task<List<CollectionModel>> GetCollectionsAsync()
		{
			CollectionModel col1 = new()
			{
				Name = "My collection one",
				LstBooks = new List<BookModel>()
				{
					new BookModel
					{
						Name = "Diverta"
					},
					new BookModel
					{
						Name = "Cartea mea de citate"
					}
				}
			};

			CollectionModel col2 = new()
			{
				Name = "My collection two",
				LstBooks = new List<BookModel>()
				{
					new BookModel
					{
						Name = "MIzerabiii"
					},
					new BookModel
					{
						Name = "Harry potter 4"
					},
					new BookModel
					{
						Name = "Alte carti pentru alte dati"
					}
				}
			};
			return new List<CollectionModel> { col1, col2 };
		}

		public Task SaveAsync(CollectionModel collectionModel)
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync(BookModel collectionModel)
		{
			throw new NotImplementedException();
		}
	}
}
