using Domain.Entities;
using Domain.ViewModels;

namespace BLL.Interfaces;

public interface IReservationRepository
{
    void AddReservation(Reservation reservation);
    List<Reservation> GetAllReservations();
    List<ReservationView> GetReservationViews();
    void UpdateReservationStatus(int reservationId, string status);
    List<ReservationView> GetTeacherReservations(int teacherUserId, string? status);
    List<ReservationView> GetAllReservationsByStatus(string? status);
}