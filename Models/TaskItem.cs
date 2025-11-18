namespace ASP.NET_Core_REST_API.Models
{
	public class TaskItem
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool IsCompleted { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? CompletedAt { get; set; }

		public Guid UserId { get; set; }

		public User User { get; set; } = null!;
	}
}