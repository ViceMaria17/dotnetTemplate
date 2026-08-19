using System;
using AppProject.Models.CustomValidators;

namespace AppProject.Models;

public class DeleteRequest<TIdtype> : IRequest
{
    [RequiredGuid]
    required public TIdtype Id { get; set; }
}
