extends Node2D

#dictionarry for holding the camera positions.
@export var _cameraPositions: Dictionary

var _targetPos: Vector2
var _currentPos: Vector2

var _moveCamera: bool = false

# Called when the node enters the scene tree for the first time.
func _ready():
	#caches the start position as our target so we know where we are,
	#and once we provide an input we know where to go. 
	#_targetPos = _startPos
	_targetPos = _cameraPositions.startPos

func _input(event):

	#extra fail safe that prevents us from trying to move if we are not at our target position. 
	if _currentPos != _targetPos: return
	
	#if the camera is not currently moving
	if !_moveCamera:
		# x input event will change the target pos based on where it currently is.
		if Input.is_action_pressed("MoveLeft"):
			if _currentPos == _cameraPositions.startPos:		#if we're at our starting position: 
				_targetPos = _cameraPositions.leftPos			#We move to the left position.
			if _currentPos == _cameraPositions.rightPos:	#But if we're at the right position:
				_targetPos = _cameraPositions.startPos		#We move back to the starting position. 
				
		if Input.is_action_pressed("MoveRight"):
			if _currentPos == _cameraPositions.startPos:
				_targetPos = _cameraPositions.rightPos
			if _currentPos == _cameraPositions.leftPos:
				_targetPos = _cameraPositions.startPos
				
		if Input.is_action_pressed("MoveUp"):
			if _currentPos == _cameraPositions.startPos:
				_targetPos = _cameraPositions.upPos
				
		if Input.is_action_pressed("MoveDown"):
			if _currentPos == _cameraPositions.upPos:
				_targetPos = _cameraPositions.startPos
		
		#Once an input is given and we have our target position, we tell the camera to move.
		_moveCamera = true



func _process(delta):
	
	#Since Lerping/Tweening doesn't ever actually reach the exact value we want to go to,
	#We have to tell the camera that once your near your target value to just clamp there.
	#Otherwise it gets infinitely closer to it's target position. 
	
	#Depending on which target vector2 we're passing through:
	match _targetPos:
		_cameraPositions.leftPos:
			if position.x <= (_targetPos.x + 1):	#We check if the position is within a given threshold.
				position.x = _targetPos.x			#Then we clamp it to our desired position
				
		_cameraPositions.rightPos:
			if position.x >= (_targetPos.x - 1):
				position.x = _targetPos.x
				
		_cameraPositions.upPos:
			if position.y <= (_targetPos.y + 1):
				position.y = _targetPos.y
			pass

		_cameraPositions.startPos:
			#We check whcih position we're moving to the start position from.
			if _currentPos == _cameraPositions.leftPos:	
				if position.x >= (_targetPos.x - 1) and position.x <= (_targetPos.x + 1):	#We then check if x/y position is within threshold
					position.x = _targetPos.x												#then we just clamp it to the start position. 
			elif _currentPos == _cameraPositions.rightPos:
				if position.x <= (_targetPos.x + 1) and position.x >= (_targetPos.x - 1):
					position.x = _targetPos.x
			elif _currentPos == _cameraPositions.upPos:
				if position.y >=  (_targetPos.y - 1):
					position.y = _targetPos.y 
			pass
	
	
	if position == _targetPos: #Once our position equals our target position,
		_currentPos = _targetPos #we set our current position to our target so we know where we are.
		_moveCamera = false		#Then we tell the camera it can no longer move. 


func _physics_process(delta):
	
	if _moveCamera: #if the Camera3D can move.
		position = lerp(position, _targetPos, 15 * delta) #we move the object to it's targeted position. 
	
#ISSUE LOG
	##ISSUE 1: This works however when you press the buttons too fast it breaks and gets stuck moving
			#to an infinite location.
	##SOLUTION: kekw the issue was using tween, just use the lerp . 
