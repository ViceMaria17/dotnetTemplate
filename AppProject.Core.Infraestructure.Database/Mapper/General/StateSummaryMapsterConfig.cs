using System;
using AppProject.Core.Infraestructure.Database.Entities.General;
using AppProject.Core.Models.General;
using Mapster;

namespace AppProject.Core.Infraestructure.Database.Mapper.General;

public class StateSummaryMapsterConfig : IRegisterMapsterConfig
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TbState, StateSummary>()
            .Map(dest => dest.CountryName, src => src.Country.Name);
    }
}
