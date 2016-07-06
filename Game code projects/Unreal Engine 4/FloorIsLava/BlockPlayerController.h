#pragma once

#include "GameFramework/PlayerController.h"
#include "BlockPlayerController.generated.h"

/**
 * 
 */
UCLASS()
class FLOORISLAVA_API ABlockPlayerController : public APlayerController
{
	GENERATED_BODY()

public:
	virtual void SetupInputComponent() override;
	
protected:
	void OnJump();
	void MoveForward(float AxisValue);
	void MoveRight(float AxisValue);
	void LookUp(float AxisValue);
	void Turn(float AxisValue);
	
};
