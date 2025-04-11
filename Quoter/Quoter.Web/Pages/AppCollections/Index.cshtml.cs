using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;
using System.Threading.Tasks;

namespace Quoter.Web.Pages.AppCollections
{
	public class IndexModel : PageModel
	{
		private readonly ILogger _logger;
		private readonly ApplicationDbContext _context;
		private readonly IFileUploadService _fileUploadService;

		public int TotalRecords { get; set; } = 0;

		[BindProperty(SupportsGet = true)]
		public int PageNo { get; set; } = 1;

		[BindProperty(SupportsGet = true)]
		public int PageSize { get; set; } = 10;

		public IList<AppCollection> AppCollections { get; set; } = new List<AppCollection>();

		public IndexModel(ILoggerFactory loggerFactory, ApplicationDbContext context, IFileUploadService fileUploadService)
		{
			_logger = loggerFactory.CreateLogger("AppCollections.Index");
			_context = context;
			_fileUploadService = fileUploadService;
		}

		public async Task OnGet()
		{
			TotalRecords = await _context.AppCollections.CountAsync();

			AppCollections = await _context.AppCollections
				.AsNoTracking()
				.OrderByDescending(c => c.UploadDateTime)
				.Skip((PageNo - 1) * PageSize)
				.Take(PageSize)
				.ToListAsync();
		}

		public async Task<ActionResult> OnGetDownloadAsync(int id)
		{
			try
			{
				AppCollection? collection = await _context.AppCollections
					.AsNoTracking()
					.Where(c => c.Id == id)
					.FirstOrDefaultAsync();

				if (!System.IO.File.Exists(collection?.Path))
				{
					throw new ArgumentException($"File does not exist {collection?.Path} for collection id:{id}");
				}

				byte[] bytes = System.IO.File.ReadAllBytes(collection.Path);
				return File(bytes, "application/octet-stream", Path.GetFileName(collection.Path));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occured while a user tried to download a collection id:{}", id);
				return BadRequest();
			}
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			try
			{
				AppCollection? collection = await _context.AppCollections.FirstOrDefaultAsync(c => c.Id == id);

				if (collection is null)
				{
					_logger.LogWarning("An attempt was made to delete {entity} with an invalid {property}={value}", nameof(AppCollection), nameof(AppCollection.Id), id);
					return BadRequest();
				}
				else
				{
					_fileUploadService.Delete(collection.Path);
					_context.AppCollections.Remove(collection);
					await _context.SaveChangesAsync();
				}
				return RedirectToPage();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occured while trying to delete {entity} with {property}={value}", nameof(AppCollection), nameof(AppCollection.Id), id);
				return StatusCode(500);
			}
		}
	}
}
