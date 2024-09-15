using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Utilities.Email.Dtos
{
    public class EmailDto
    {
        public class EmailGetDto : IDto
        {
            public string SenderMail { get; set; } = string.Empty;
            public string SenderPassword { get; set; } = string.Empty;
            public int Port { get; set; }
            public string Host { get; set; } = string.Empty;
            public bool EnableSsl { get; set; }
        }

        public class EmailPostArrayDto : IDto
        {
            public List<string> ReceiverEmails { get; set; } = new List<string>();
            public string Subject { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
        }

        public class EmailPostDto : IDto
        {
            public string ReceiverEmail { get; set; } = string.Empty;
            public string Subject { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
        }

        public class EmailSendResultDto
        {
            public string Email { get; set; } = string.Empty;
            public bool IsSuccess { get; set; }
            public string ErrorMessage { get; set; } = string.Empty;
        }
    }
}
