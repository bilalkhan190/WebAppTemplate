using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Shared.Enums;

namespace WebAppTemplate.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid? UpdatedBy { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DeletionStatus Deleted { get; set; }
        public Status Active { get; set; }

        public void SetCreatedDefaults(Guid createdBy)
        {
            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;
            Active = Status.Active;
            Deleted = DeletionStatus.NotDeleted;
        }

        public void SetUpdatedDefaults(Guid updatedBy)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }
        public void SetDeletedDefaults(Guid deletedBy)
        {
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deletedBy;
            Deleted = DeletionStatus.Deleted;
        }

       
    }
}
