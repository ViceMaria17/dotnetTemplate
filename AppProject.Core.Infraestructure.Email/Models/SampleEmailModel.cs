using System;

namespace AppProject.Core.Infraestructure.Email.Models;

public class SampleEmailModel
{
    public string Name { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.UtcNow;
}
