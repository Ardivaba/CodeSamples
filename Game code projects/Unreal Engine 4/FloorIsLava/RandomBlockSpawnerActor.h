// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "RandomBlockSpawnerActor.generated.h"

UCLASS()
class FLOORISLAVA_API ARandomBlockSpawnerActor : public AActor
{
	GENERATED_BODY()
	
public:	
	ARandomBlockSpawnerActor();
	virtual void BeginPlay() override;
	virtual void Tick( float DeltaSeconds ) override;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Spawner)
	UClass* BlockActorClass;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Spawner)
	float InitialSpawnTime = 1.0f;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Spawner)
	int32 SpawnerWidthX = 10;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Spawner)
	int32 SpawnerWidthY = 10;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Spawner)
	float BlockSize = 250.0f;

private:
	float timePassedBetweenSpawns = 0.0f;
	void SpawnRandomBlock();

	void SpawnBlockAt(int PosX, int PosY);
	
};
