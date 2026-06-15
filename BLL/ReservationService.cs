using BLL.Interfaces;
using Domain.Entities;
using Domain.ViewModels;

namespace BLL;

public class ReservationService
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(
        IClassroomRepository classroomRepository,
        IReservationRepository reservationRepository)
    {
        _classroomRepository = classroomRepository;
        _reservationRepository = reservationRepository;
    }

    public List<Classroom> GetAvailableClassrooms()
    {
        return _classroomRepository.GetAvailableClassrooms();
    }

    public void CreateReservation(
        int userId,
        int classroomId,
        string subject,
        DateTime startDateTime,
        DateTime endDateTime)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException("Please fill in the subject before reserving.");
        }

        if (startDateTime == default || endDateTime == default)
        {
            throw new ArgumentException("Please select a date and time slot before reserving.");
        }

        var reservation = new Reservation(
            reservationId: 0,
            teacherUserId: userId,
            classroomId: classroomId,
            subject: subject,
            startDateTime: startDateTime,
            endDateTime: endDateTime,
            dateRequested: DateTime.Now,
            status: "Pending"
        );

        Console.WriteLine("Service reached before repository");

        _reservationRepository.AddReservation(reservation);
    }

    public List<Reservation> GetAllReservations()
    {
        return _reservationRepository.GetAllReservations();
    }

    public List<ReservationView> GetReservationViews()
    {
        return _reservationRepository.GetReservationViews();
    }

    public List<ReservationView> GetTeacherReservations(int teacherUserId, string? status)
    {
        return _reservationRepository.GetTeacherReservations(teacherUserId, status);
    }

    public List<ReservationView> GetAllReservationsByStatus(string? status)
    {
        return _reservationRepository.GetAllReservationsByStatus(status);
    }
}