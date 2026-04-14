using Domain;

namespace DAL;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddReservation(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        _context.SaveChanges();
    }

    public List<Reservation> GetAllReservations()
    {
        return _context.Reservations.ToList();
    }
}