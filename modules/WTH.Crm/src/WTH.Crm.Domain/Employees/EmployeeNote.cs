using System;
using Volo.Abp.Domain.Entities;

namespace Wth.Crm.Employees
{
    public class EmployeeNote : Entity
    {

        public Guid EmployeeId { get; protected set; }

        public Guid NoteId { get; protected set; }

        private EmployeeNote()
        {

        }

        public EmployeeNote(Guid employeeId, Guid noteId)
        {
            EmployeeId = employeeId;
            NoteId = noteId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    EmployeeId,
                    NoteId
                };
        }
    }
}