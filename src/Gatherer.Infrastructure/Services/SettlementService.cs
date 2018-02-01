using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;
using Gatherer.Infrastructure.DTO;
using Gatherer.Infrastructure.Extensions;

namespace Gatherer.Infrastructure.Services
{
    public class SettlementService : ISettlementService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISettlementRepository _settlementRepository;
        private readonly IMapper _mapper;

        public SettlementService(ISettlementRepository settlementRepository, IUserRepository userRepository, IMapper mapper)
        {
            _settlementRepository = settlementRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<SettlementDetailsDto> GetAsync(Guid id)
        {
            // var user = await _userRepository.GetOrFailAsync(userId);
            var settlement = await _settlementRepository.GetAsync(id);

            return _mapper.Map<SettlementDetailsDto>(settlement);
        }

        // public async Task<IEnumerable<SettlementDetailsDto>> GetForUserAsync(Guid userId)
        // {
        //     var user = await
        // }

        public async Task CreateAsync(Guid id, Guid userId, string name, string description = null)
        {
            var settlement = await _settlementRepository.GetAsync(id);
            if (settlement != null)
            {
                throw new Exception($"Settlement with id: '{id}' already exist.");
            }
            settlement = new Settlement(id, userId, name, description);
            await _settlementRepository.AddAsync(settlement);
        }

        public async Task UpdateAsync(Guid settlementId, string name, string description)
        {
            var settlement = await _settlementRepository.GetSettlementOrFailAsync(settlementId);
            settlement.SetName(name);
            settlement.SetDescription(description);
            await _settlementRepository.UpdateSettlementAsync(settlement);
        }

        public async Task DeleteAsync(Guid settlementId)
        {
            var settlement = await _settlementRepository.GetSettlementOrFailAsync(settlementId);
            await _settlementRepository.DeleteSettlementAsync(settlement);
        }
    }
}