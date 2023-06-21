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
	_targetPos = _startPos
	pass # Replace with function body.


func _input(event):
	#if the camera is not currently moving
	if !_moveCamera:
		# x input will change the target pos based on where it currently is.
		if Input.is_action_pressed("MoveLeft"):
			if _targetPos == _startPos:
				_targetPos = _lookLeftPos	
			if _targetPos == _lookRightPos:
				_targetPos = _startPos
				
		if Input.is_action_pressed("MoveRight"):
			if _targetPos == _startPos:
				_targetPos = _lookRightPos
			if _targetPos == _lookLeftPos:
				_targetPos = _startPos
				
		if Input.is_action_pressed("MoveUp"):
			if _targetPos == _startPos:
				_targetPos = _lookUpPos
				
		if Input.is_action_pressed("MoveDown"):
			if _targetPos == _lookUpPos:
				_targetPos = _startPos
		
		#then, tell the camera to tween to it's targeted destination
		_moveCamera = true



func _process(delta):
	#Depending on which target vector2 we're passing through
	match _targetPos:
		_lookLeftPos:
			if position.x <= (_targetPos.x + 1):
				position.x = _targetPos.x

		_lookRightPos:
			if position.x >= (_targetPos.x - 1):
				position.x = _targetPos.x

		_lookUpPos:
			pass
		##ISSUE: This works however when you press the buttons too fast it breaks and gets stuck moving
				#to an infinite location.
		##SOLUTION: Maybe we don't need the tween funciton inside of physics process?
				#since we're not using the lerp any more. 
		_startPos:
			if _currentPos == _lookLeftPos:
				if position.x >= (_targetPos.x - 1):
					position.x = _targetPos.x
			elif _currentPos == _lookRightPos:
				if position.x <= (_targetPos.x +1):
					position.x = _targetPos.x
			pass
	
	
	if position == _targetPos:
		_currentPos = _targetPos
		_moveCamera = false
		pass
	print(position.x)
	pass

func _physics_process(delta):
	if _moveCamera:
		var tween := create_tween()
		tween.tween_property(self, "position", _targetPos, 0.10)
		#position = lerp(position, _targetPos, 25 * delta)
		pass
	pass
	
