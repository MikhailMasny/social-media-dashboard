namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Observation data transfer object.
    /// </summary>
    public class ObservationDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
