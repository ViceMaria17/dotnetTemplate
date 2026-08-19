using System;
using Mapster;

namespace AppProject.Core.Infraestructure.Database.Mapper;

public interface IRegisterMapsterConfig
{
    void Register(TypeAdapterConfig config);
}
