using Domain;

namespace DAL;

public class ReservationRepository : IReservationRepository
{
    private readonly List<Reservation> _reservations = new();

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }

    public List<Reservation> GetAllReservations()
    {
        return new List<Reservation>(_reservations);
    }
}