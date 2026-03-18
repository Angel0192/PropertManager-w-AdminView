public class RentPaymentsController : Controller
{
    private readonly ApplicationDbContext _context;
    public RentPaymentsController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index() => 
        View(await _context.RentPayments.Include(r => r.RentSchedule).ToListAsync());

    public IActionResult Create()
    {
        // Dropdown showing Schedule ID or a description
        ViewBag.ScheduleID = new SelectList(_context.RentSchedules, "ScheduleID", "ScheduleID");
        return View();
    }

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
        return View(rentPayment);
    }
}