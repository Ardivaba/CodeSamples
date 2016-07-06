// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/HUD.h"
#include "GameHUD.generated.h"

/**
 * 
 */
UCLASS()
class FLOORISLAVA_API AGameHUD : public AHUD
{
	GENERATED_BODY()
	
	
public:
	virtual void BeginPlay() override;
};
