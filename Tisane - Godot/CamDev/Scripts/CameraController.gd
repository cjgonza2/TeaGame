extends Node2D


export var _startPos: Vector2
export var _lookUpPos: Vector2
export var _lookLeftPos: Vector2
export var _lookRightPos: Vector2
var _targetPos: Vector2
var _currentPos: Vector2

var _moveCamera: bool = false



# Called when the node enters the scene tree for the first time.
func _ready():
	#caches the start position as our target so we know where we are,
	#and once we provide an input we know where to go. 
	_targetPos = _startPos

func _input(event):

	#extra fail safe that prevents us from trying to move if we are not at our target position. 
	if _currentPos != _targetPos: return
	
	#if the camera is not currently moving
	if !_moveCamera:
		# x input event will change the target pos based on where it currently is.
		if Input.is_action_pressed("MoveLeft"):
			if _currentPos == _startPos:		#if we're at our starting position: 
				_targetPos = _lookLeftPos		#We move to the left position.
			if _currentPos == _lookRightPos:	#But if we're at the right position:
				_targetPos = _startPos			#We move back to the starting position. 
				
		if Input.is_action_pressed("MoveRight"):
			if _currentPos == _startPos:
				_targetPos = _lookRightPos
			if _currentPos == _lookLeftPos:
				_targetPos = _startPos
				
		if Input.is_action_pressed("MoveUp"):
			if _currentPos == _startPos:
				_targetPos = _lookUpPos
				
		if Input.is_action_pressed("MoveDown"):
			if _currentPos == _lookUpPos:
				_targetPos = _startPos
		
		#Once an input is given and we have our target position, we tell the camera to move.
		_moveCamera = true



func _process(delta):
	
	#Since Lerping/Tweening doesn't ever actually reach the exact value we want to go to,
	#We have to tell the camera that once your near your target value to just clamp there.
	#Otherwise it gets infinitely closer to it's target position. 
	
	#Depending on which target vector2 we're passing through:
	match _targetPos:
		_lookLeftPos:
			if position.x <= (_targetPos.x + 1):	#We check if the position is within a given threshold.
				position.x = _targetPos.x			#Then we clamp it to our desired position
				
		_lookRightPos:
			if position.x >= (_targetPos.x - 1):
				position.x = _targetPos.x
				
		_lookUpPos:
			if position.y <= (_targetPos.y + 1):
				position.y = _targetPos.y
			pass

		_startPos:
			#We check whcih position we're moving to the start position from.
			if _currentPos == _lookLeftPos:	
				if position.x >= (_targetPos.x - 1) and position.x <= (_targetPos.x + 1):	#We then check if x/y position is within threshold
					position.x = _targetPos.x												#then we just clamp it to the start position. 
			elif _currentPos == _lookRightPos:
				if position.x <= (_targetPos.x + 1) and position.x >= (_targetPos.x - 1):
					position.x = _targetPos.x
			elif _currentPos == _lookUpPos:
				if position.y >=  (_targetPos.y - 1):
					position.y = _targetPos.y 
			pass
	
	
	if position == _targetPos: #Once our position equals our target position,
		_currentPos = _targetPos #we set our current position to our target so we know where we are.
		_moveCamera = false		#Then we tell the camera it can no longer move. 


func _physics_process(delta):
	
	if _moveCamera: #if the Camera can move.
		position = lerp(position, _targetPos, 15 * delta) #we move the object to it's targeted position. 
	
#ISSUE LOG
	##ISSUE 1: This works however when you press the buttons too fast it breaks and gets stuck moving
			#to an infinite location.
	##SOLUTION: kekw the issue was using tween, just use the lerp . 
