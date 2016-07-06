// Fill out your copyright notice in the Description page of Project Settings.

#include "FloorIsLava.h"
#include "BlockPlayerController.h"
#include "BlockCharacter.h"

void ABlockPlayerController::SetupInputComponent()
{
	Super::SetupInputComponent();

	InputComponent->BindAction("Jump", IE_Pressed, this, &ABlockPlayerController::OnJump);
	InputComponent->BindAxis("Forward", this, &ABlockPlayerController::MoveForward);
	InputComponent->BindAxis("Right", this, &ABlockPlayerController::MoveRight);
	InputComponent->BindAxis("Turn", this, &ABlockPlayerController::Turn);
	InputComponent->BindAxis("LookUp", this, &ABlockPlayerController::LookUp);
}

void ABlockPlayerController::OnJump()
{
	ABlockCharacter* playerPawn = Cast<ABlockCharacter>(GetControlledPawn());
	if (playerPawn != nullptr)
	{
		playerPawn->OnJump();
	}
}

void ABlockPlayerController::MoveForward(float AxisValue)
{
	ABlockCharacter* playerPawn = Cast<ABlockCharacter>(GetControlledPawn());
	if (playerPawn != nullptr)
	{
		playerPawn->MoveForward(AxisValue);
	}
}

void ABlockPlayerController::MoveRight(float AxisValue)
{
	ABlockCharacter* playerPawn = Cast<ABlockCharacter>(GetControlledPawn());
	if (playerPawn != nullptr)
	{
		playerPawn->MoveRight(AxisValue);
	}
}

void ABlockPlayerController::LookUp(float AxisValue)
{
	ABlockCharacter* playerPawn = Cast<ABlockCharacter>(GetControlledPawn());
	if (playerPawn != nullptr)
	{
		playerPawn->AddControllerPitchInput(AxisValue);
	}
}

void ABlockPlayerController::Turn(float AxisValue)
{
	ABlockCharacter* playerPawn = Cast<ABlockCharacter>(GetControlledPawn());
	if (playerPawn != nullptr)
	{
		playerPawn->AddControllerYawInput(AxisValue);
	}
}


