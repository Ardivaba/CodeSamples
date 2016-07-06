#include "FloorIsLava.h"
#include "BlockCharacter.h"


ABlockCharacter::ABlockCharacter()
{
	PrimaryActorTick.bCanEverTick = true;

}

void ABlockCharacter::BeginPlay()
{
	Super::BeginPlay();
	
}

void ABlockCharacter::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

}

void ABlockCharacter::OnJump()
{
	ACharacter::Jump();
}

void ABlockCharacter::MoveForward(float AxisValue)
{
	FVector moveDirection = GetActorForwardVector();
	ABlockCharacter::AddMovementInput(moveDirection, AxisValue);
}

void ABlockCharacter::MoveRight(float AxisValue)
{
	FVector moveDirection = GetActorRightVector();
	ABlockCharacter::AddMovementInput(moveDirection, AxisValue);
}

