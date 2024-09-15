using Wth.Crm.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Wth.Crm.Companies
{
    public abstract class CompanyManagerBase : DomainService
    {
        protected ICompanyRepository _companyRepository;
        protected IRepository<Note, Guid> _noteRepository;

        public CompanyManagerBase(ICompanyRepository companyRepository,
        IRepository<Note, Guid> noteRepository)
        {
            _companyRepository = companyRepository;
            _noteRepository = noteRepository;
        }

        public virtual async Task<Company> CreateAsync(
        List<Guid> noteIds,
        string name, string? taxReference = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var company = new Company(
             GuidGenerator.Create(),
             name, taxReference
             );

            await SetNotesAsync(company, noteIds);

            return await _companyRepository.InsertAsync(company);
        }

        public virtual async Task<Company> UpdateAsync(
            Guid id,
            List<Guid> noteIds,
        string name, string? taxReference = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var queryable = await _companyRepository.WithDetailsAsync(x => x.Notes);
            var query = queryable.Where(x => x.Id == id);

            var company = await AsyncExecuter.FirstOrDefaultAsync(query);

            company.Name = name;
            company.TaxReference = taxReference;

            await SetNotesAsync(company, noteIds);

            company.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _companyRepository.UpdateAsync(company);
        }

        private async Task SetNotesAsync(Company company, List<Guid> noteIds)
        {
            if (noteIds == null || !noteIds.Any())
            {
                company.RemoveAllNotes();
                return;
            }

            var query = (await _noteRepository.GetQueryableAsync())
                .Where(x => noteIds.Contains(x.Id))
                .Select(x => x.Id);

            var noteIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!noteIdsInDb.Any())
            {
                return;
            }

            company.RemoveAllNotesExceptGivenIds(noteIdsInDb);

            foreach (var noteId in noteIdsInDb)
            {
                company.AddNote(noteId);
            }
        }

    }
}