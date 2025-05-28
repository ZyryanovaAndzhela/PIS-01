using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gos_uslugi.Services
{
    class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;

        public RuleService(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<List<ServiceRule>> GetServiceRules(long serviceId)
        {
            return await _ruleRepository.GetServiceRules(serviceId);
        }
        public async Task<ServiceRule> SaveServiceRule(ServiceRule serviceRule)
        {
            return await _ruleRepository.SaveServiceRule(serviceRule);
        }
        public async Task UpdateServiceRule(ServiceRule serviceRule)
        {
            await _ruleRepository.UpdateServiceRule(serviceRule);
        }
    }
}
