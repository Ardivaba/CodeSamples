// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Character.h"
#include "BlockCharacter.generated.h"

UCLASS()
class FLOORISLAVA_API ABlockCharacter : public ACharacter
{
	GENERATED_BODY()

public:
	ABlockCharacter();

public:
	virtual void BeginPlay() override;
	virtual void Tick( float DeltaSeconds ) override;

	void OnJump();
	void MoveForward(float AxisValue);
	void MoveRight(float AxisValue);
};
