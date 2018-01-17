using AutoMapper;
using Gatherer.Core.Domain;
using Gatherer.Infrastructure.DTO;

namespace Gatherer.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Settlement, SettlementDto>();
                cfg.CreateMap<Settlement, SettlementDetailsDto>();
                cfg.CreateMap<Expense, ExpenseDto>();
            })
            .CreateMapper();
    }
}