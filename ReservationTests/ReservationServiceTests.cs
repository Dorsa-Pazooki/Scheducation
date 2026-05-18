using BLL;
using DAL;
using Domain.Entities;
using Domain.ViewModels;

namespace ReservationTests;

public class ReservationServiceTests
{
    [Test]
    public void CreateReservation_ShouldSaveReservation()
    {
        // Arrange
        var fakeClassroomRepository = new FakeClassroomRepository();
        var fakeReservationRepository = new FakeReservationRepository();

        var reservationService = new ReservationService(
            fakeClassroomRepository,
            fakeReservationRepository);

        // Act
        reservationService.CreateReservation(
            userId: 1,
            classroomId: 1,
            subject: "Math",
            reservationTime: DateTime.Now
        );

        // Assert
        Assert.That(fakeReservationRepository.Reservations.Count, Is.EqualTo(1));
    }
}

public class FakeReservationRepository : IReservationRepository
{
    public List<Reservation> Reservations = new();

    public void AddReservation(Reservation reservation)
    {
        Reservations.Add(reservation);
    }

    public List<Reservation> GetAllReservations()
    {
        return Reservations;
    }

    public List<ReservationView> GetReservationViews()
    {
        return new List<ReservationView>();
    }
}

public class FakeClassroomRepository : IClassroomRepository
{
    public List<Classroom> GetAvailableClassrooms()
    {
        return new List<Classroom>();
    }
}