using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace WTH.Training.AwardTypes
{
    public abstract class AwardTypeManagerBase : DomainService
    {
        protected IAwardTypeRepository _awardTypeRepository;

        public AwardTypeManagerBase(IAwardTypeRepository awardTypeRepository)
        {
            _awardTypeRepository = awardTypeRepository;
        }

        public virtual async Task<AwardType> CreateAsync(
        string name, bool hasReferenceNumber, bool hasExpiryDate)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var awardType = new AwardType(
             GuidGenerator.Create(),
             name, hasReferenceNumber, hasExpiryDate
             );

            return await _awardTypeRepository.InsertAsync(awardType);
        }

        public virtual async Task<AwardType> UpdateAsync(
            Guid id,
            string name, bool hasReferenceNumber, bool hasExpiryDate, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var awardType = await _awardTypeRepository.GetAsync(id);

            awardType.Name = name;
            awardType.HasReferenceNumber = hasReferenceNumber;
            awardType.HasExpiryDate = hasExpiryDate;

            awardType.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _awardTypeRepository.UpdateAsync(awardType);
        }

    }
}