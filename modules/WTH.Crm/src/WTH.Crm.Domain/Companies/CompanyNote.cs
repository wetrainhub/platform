using System;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Companies
{
    public class CompanyNote : Entity
    {

        public Guid CompanyId { get; protected set; }

        public Guid NoteId { get; protected set; }

        private CompanyNote()
        {

        }

        public CompanyNote(Guid companyId, Guid noteId)
        {
            CompanyId = companyId;
            NoteId = noteId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    CompanyId,
                    NoteId
                };
        }
    }
}