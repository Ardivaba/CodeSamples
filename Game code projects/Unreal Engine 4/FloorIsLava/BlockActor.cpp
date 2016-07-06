// Fill out your copyright notice in the Description page of Project Settings.

#include "FloorIsLava.h"
#include "BlockActor.h"
#include "BlocksGameMode.h"
#include "LavaActor.h"

// Sets default values
ABlockActor::ABlockActor()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}

// Called when the game starts or when spawned
void ABlockActor::BeginPlay()
{
	Super::BeginPlay();
}

// Called every frame
void ABlockActor::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

	SleepTime -= DeltaTime;
	DeathTime -= DeltaTime;

	if (SleepTime <= 0.0f)
	{
		auto Component = Super::GetComponentByClass(UStaticMeshComponent::StaticClass());
		UStaticMeshComponent* MeshComponent = Cast<UStaticMeshComponent>(Component);
		
		MeshComponent->SetSimulatePhysics(false);
	}

	if (DeathTime <= 0.0f)
		Super::Destroy();
}

void ABlockActor::NotifyHit(class UPrimitiveComponent* MyComp, AActor* Other, class UPrimitiveComponent* OtherComp, bool bSelfMoved, FVector HitLocation, FVector HitNormal, FVector NormalImpulse, const FHitResult& Hit)
{
	ALavaActor* LavaActor = Cast<ALavaActor>(Other);
	if(LavaActor)
	{
		//Super::Destroy(true);
	}
}

FVector ABlockActor::GetRandomBlockColor()
{
	FVector RandomColor;

	RandomColor.X = FMath::FRandRange(0.0f, 1.0f);
	RandomColor.Y = FMath::FRandRange(0.0f, 1.0f);
	RandomColor.Z = FMath::FRandRange(0.0f, 1.0f);

	return RandomColor;
}

