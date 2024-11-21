
using FluentAssertions;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.Aggregate;

namespace UnitTests;

public class DomainTests
{
    // [Fact]
    // public void Move_Pets_Should_Be_Not_To_Move_When_Pet_Already_At_New_Position()
    // {
    //     // arrange
    //     var volunteer = CreateVolunteerWithPets(6);
    //     var newPosition = Position.Create(2).Value;
    //     var petToMove = volunteer.Pets![1];
    //
    //     // act
    //     var result = volunteer.MovePet(petToMove, newPosition);
    //
    //     // assert
    //     result.IsSuccess.Should().BeTrue();
    //     PetsPositions(volunteer, 1, 2, 3, 4, 5, 6);
    // }
    //
    // [Fact]
    // public void Move_Pets_Should_Be_Move_Pet_At_Last_Position()
    // {
    //     // arrange
    //     var volunteer = CreateVolunteerWithPets(6);
    //     var newPosition = Position.Create(volunteer.Pets!.Count).Value;
    //     var petToMove = volunteer.Pets![1];
    //
    //     // act
    //     var result = volunteer.MovePet(petToMove, newPosition);
    //
    //     // assert
    //     result.IsSuccess.Should().BeTrue();
    //     PetsPositions(volunteer, 1, 6, 2, 3, 4, 5);
    // }
    //
    // [Fact]
    // public void Move_Pets_Should_Be_Move_Pet_At_First_Position()
    // {
    //     // arrange
    //     var volunteer = CreateVolunteerWithPets(6);
    //     var newPosition = Position.Create(1).Value;
    //     var petToMove = volunteer.Pets![4];
    //
    //     // act
    //     var result = volunteer.MovePet(petToMove, newPosition);
    //
    //     // assert
    //     result.IsSuccess.Should().BeTrue();
    //     PetsPositions(volunteer, 2, 3, 4, 5, 1, 6);
    // }
    //
    // private Volunteer CreateVolunteerWithPets(int countOfPets)
    // {
    //     var volunteer = new Volunteer(
    //         VolunteerId.NewVolonteerId(), null, null, null, null, null, null, null);
    //
    //     // for (var i = 0; i < countOfPets; i++)
    //     // {
    //     //     var pet = new Pet(
    //     //         PetId.NewPetId(), null, null, null, null, null, null,
    //     //         null, null, true, true, null, DateTime.Today, null, null,
    //     //         PetStatus.FoundHome);
    //     //
    //     //     volunteer.AddPet(pet);
    //     // }
    //
    //     return volunteer;
    // }
    //
    // private void PetsPositions(Volunteer volunteer, params int[] positions)
    // {
    //     for (var i = 0; i < positions.Length; i++)
    //         volunteer.Pets![i].Position.Value.Should().Be(positions[i]);
    // }
}