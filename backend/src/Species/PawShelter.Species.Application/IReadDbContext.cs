﻿using PawShelter.Core.Dto;

namespace PawShelter.Species.Application;

public interface IReadDbContext
{
    IQueryable<SpeciesDto> Species { get; }
    
    IQueryable<BreedDto> Breeds { get; }
}