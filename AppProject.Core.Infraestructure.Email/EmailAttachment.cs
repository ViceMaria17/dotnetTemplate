using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Routing.Constraints;

namespace AppProject.Core.Infraestructure.Email;

public class EmailAttachment
{
    public string Content { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Disposition { get; set; } = string.Empty;

    public string? ContentId { get; set; }
}
