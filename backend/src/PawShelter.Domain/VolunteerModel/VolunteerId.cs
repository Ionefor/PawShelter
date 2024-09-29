﻿namespace PawShelter.Domain.VolunteerModel
{
    public record VolunteerId
    {
        private VolunteerId(Guid value) => Value = value;
        public Guid Value { get; }
        public static VolunteerId NewVolonteerId() => new(Guid.NewGuid());
        public static VolunteerId Empty() => new(Guid.Empty);
        public static VolunteerId Create(Guid id) => new(id);
    }
}
