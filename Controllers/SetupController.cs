using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickPortals.Data;

namespace QuickPortals.Controllers
{
    public class SetupController : Controller
    {
        private readonly AppDbContext _db;
        private readonly SqlDataAccess _sqlData;

        public SetupController(AppDbContext db, SqlDataAccess sqlData)
        {
            _db = db;
            _sqlData = sqlData;
        }

        public async Task<IActionResult> Index()
        {
            // EF Core – use LINQ to get first message
            var efMessage = (await _db.SetupChecks
                .OrderBy(s => s.Id)
                .FirstOrDefaultAsync())
                ?.Message ?? "No EF Core data";

            // ADO.NET – raw query
            var adoMessage = await _sqlData.GetFirstSetupMessageAsync();

            ViewBag.EfMessage = efMessage;
            ViewBag.AdoMessage = adoMessage;
            return View();
        }
    }
}
