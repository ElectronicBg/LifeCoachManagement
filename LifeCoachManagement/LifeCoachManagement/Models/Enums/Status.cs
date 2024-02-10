using System.ComponentModel.DataAnnotations;

namespace LifeCoachManagement.Models
{
    public enum Status
    {
        Pending,
        AssignedToCoach,
        ForReview,
        Completed,
        Cancelled
    }
}
