using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models;

public class RentPaymentsController : Controller
{
    private readonly ApplicationDbContext _context;
    public RentPaymentsController(ApplicationDbContext context) => _context = context;

    // GET: RentPayments
    public async Task<IActionResult> Index()
    {
        // Includes RentSchedule AND the Tenant linked to that schedule
        var payments = await _context.RentPayments
            .Include(r => r.RentSchedule)
                .ThenInclude(s => s.Tenant)
            .ToListAsync();
        return View(payments);
    }

    // GET: RentPayments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var payment = await _context.RentPayments
            .Include(r => r.RentSchedule)
                .ThenInclude(s => s.Tenant)
            .FirstOrDefaultAsync(m => m.PaymentID == id);

        if (payment == null) return NotFound();

        return View(payment);
    }

    // GET: RentPayments/Create
    public IActionResult Create()
    {
        PopulateSchedulesDropDownList();
        return View();
    }

    // POST: RentPayments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PaymentID,ScheduleID,PaymentDate,AmountPaid,PaymentMethod,TransactionReference")] RentPayment rentPayment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rentPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        PopulateSchedulesDropDownList(rentPayment.ScheduleID);
        return View(rentPayment);
    }

    // GET: RentPayments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var payment = await _context.RentPayments.FindAsync(id);
        if (payment == null) return NotFound();

        PopulateSchedulesDropDownList(payment.ScheduleID);
        return View(payment);
    }

    // POST: RentPayments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PaymentID,ScheduleID,PaymentDate,AmountPaid,PaymentMethod,TransactionReference")] RentPayment rentPayment)
    {
        if (id != rentPayment.PaymentID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rentPayment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(rentPayment.PaymentID)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        PopulateSchedulesDropDownList(rentPayment.ScheduleID);
        return View(rentPayment);
    }

    // GET: RentPayments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var payment = await _context.RentPayments
            .Include(r => r.RentSchedule)
                .ThenInclude(s => s.Tenant)
            .FirstOrDefaultAsync(m => m.PaymentID == id);

        if (payment == null) return NotFound();

        return View(payment);
    }

    // POST: RentPayments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var payment = await _context.RentPayments.FindAsync(id);
        if (payment != null)
        {
            _context.RentPayments.Remove(payment);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // Helper method to create the readable dropdown list
    private void PopulateSchedulesDropDownList(object selectedSchedule = null)
    {
        var schedulesQuery = _context.RentSchedules
            .Include(s => s.Tenant)
            .OrderBy(s => s.Date)
            .Select(s => new
            {
                s.ScheduleID,
                // Combines Tenant Name and Date for the dropdown text
                Description = $"{s.Tenant.LastName} - {s.Date:MM/dd/yyyy} (ID: {s.ScheduleID})"
            });

        ViewBag.ScheduleID = new SelectList(schedulesQuery.AsNoTracking(), "ScheduleID", "Description", selectedSchedule);
    }

    private bool PaymentExists(int id)
    {
        return _context.RentPayments.Any(e => e.PaymentID == id);
    }
}