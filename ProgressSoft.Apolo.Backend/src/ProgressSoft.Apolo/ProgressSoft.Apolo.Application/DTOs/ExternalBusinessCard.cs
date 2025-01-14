﻿using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Application.DTOs
{
    public abstract class ExternalBusinessCard
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required Gender Gender { get; init; }
        public required DateOnly BirthOfDate { get; init; }
        public required string Email { get; init; }
        public required string Phone { get; init; }
        public required string Address { get; init; }
    }
}
