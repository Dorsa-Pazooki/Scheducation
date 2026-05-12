using Domain.Entities;
namespace DAL;
using Domain.ViewModels;

public interface IReservationRepository
{
    void AddReservation(Reservation reservation);
    List<Reservation> GetAllReservations();
    List<ReservationView> GetReservationViews();
}