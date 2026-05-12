
using Domain.ViewModels;
using Domain.Entities;


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

    public List<ReservationView> GetReservationViews()
    {
        var reservationViews =
            from reservation in _context.Reservations
            join user in _context.Users
                on reservation.TeacherUserId equals user.UserId
            join classroom in _context.Classrooms
                on reservation.ClassroomId equals classroom.ClassroomId
            select new ReservationView
            {
                ReservationId = reservation.ReservationId,
                TeacherName = user.FirstName + " " + user.LastName,
                RoomNumber = classroom.RoomNumber,
                Subject = reservation.Subject,
                ReservationTime = reservation.ReservationTime,
                DateRequested = reservation.DateRequested,
                Status = reservation.Status
            };

        return reservationViews.ToList();
    }
}