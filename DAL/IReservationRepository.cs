using Domain;

namespace DAL;

public interface IReservationRepository
{
    void AddReservation(Reservation reservation);
    List<Reservation> GetAllReservations();
}