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
            // EF Core – use LINQ to get first setup message
            var efMessage = (await _db.SetupChecks
                .OrderBy(s => s.Id)
                .FirstOrDefaultAsync())
                ?.Message ?? "No EF Core data";

            // ADO.NET – raw query for the setup message
            var adoMessage = await _sqlData.GetFirstSetupMessageAsync();

            // EF Core – read all AuditLog records with LINQ
            var efLogs = await _db.AuditLogs
                .OrderBy(a => a.Id)
                .ToListAsync();

            // ADO.NET – read all AuditLog records with a raw query
            var adoLogs = await _sqlData.GetAuditLogsAsync();

            ViewBag.EfMessage = efMessage;
            ViewBag.AdoMessage = adoMessage;
            ViewBag.EfLogs = efLogs;
            ViewBag.AdoLogs = adoLogs;
            return View();
        }
    }
}
