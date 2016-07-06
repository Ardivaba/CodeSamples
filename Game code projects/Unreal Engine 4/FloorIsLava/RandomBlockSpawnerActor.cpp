// Fill out your copyright notice in the Description page of Project Settings.

#include "FloorIsLava.h"
#include "RandomBlockSpawnerActor.h"


// Sets default values
ARandomBlockSpawnerActor::ARandomBlockSpawnerActor()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void ARandomBlockSpawnerActor::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ARandomBlockSpawnerActor::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

	timePassedBetweenSpawns += DeltaTime;

	if (timePassedBetweenSpawns > InitialSpawnTime)
	{
		SpawnRandomBlock();
		timePassedBetweenSpawns = 0.0f;
	}

}

void ARandomBlockSpawnerActor::SpawnRandomBlock()
{
	float PosX = FMath::RandRange(-SpawnerWidthX, SpawnerWidthX);
	float PosY = FMath::RandRange(-SpawnerWidthY, SpawnerWidthY);

	ARandomBlockSpawnerActor::SpawnBlockAt(PosX, PosY);
}

void ARandomBlockSpawnerActor::SpawnBlockAt(int PosX, int PosY)
{
	FVector ActorLocation = Super::GetActorLocation();
	FVector SpawnPosition;
	FRotator SpawnRotation = FRotator(0, 0, 0);

	SpawnPosition.X = ActorLocation.X + PosX * BlockSize;
	SpawnPosition.Y = ActorLocation.Y + PosY * BlockSize;
	SpawnPosition.Z = ActorLocation.Z;

	GetWorld()->SpawnActor(BlockActorClass, &SpawnPosition, &SpawnRotation);
}
