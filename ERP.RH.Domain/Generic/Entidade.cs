using System;

namespace RH.Domain.Generic
{
    public abstract class Entity : EntityBase
    {
        protected Entity()
        {
            SetChildClass(this);
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
    }
}