using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChanLap.Model
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual int ModifiedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
