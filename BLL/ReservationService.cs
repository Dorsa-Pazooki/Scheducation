using DAL;
using Domain;

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

    public void CreateReservation(int userId, int classroomId, string subject, DateTime reservationTime)
    {
        var reservation = new Reservation(
            reservationId: new Random().Next(1, 100000),
            teacherId: userId,
            classroomId: classroomId,
            subject: subject,
            reservationTime: reservationTime,
            status: "Pending"
        );

        _reservationRepository.AddReservation(reservation);
    }

    public List<Reservation> GetAllReservations()
    {
        return _reservationRepository.GetAllReservations();
    }
}