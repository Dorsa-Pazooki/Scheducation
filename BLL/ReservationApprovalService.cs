using BLL.Interfaces;

namespace BLL;

public class ReservationApprovalService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationApprovalService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public void ApproveReservation(int reservationId)
    {
        _reservationRepository.UpdateReservationStatus(reservationId, "Approved");
    }

    public void RejectReservation(int reservationId)
    {
        _reservationRepository.UpdateReservationStatus(reservationId, "Rejected");
    }
}