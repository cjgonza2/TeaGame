extends Node2D

###CONSTANT VARs
const _default = preload("res://CamDev/Sprites/PlayerTools/TeaPot/teapot_body_empty.png")

#I wish I was joking other than creating a sprite sheet I think this is the only way to cache
#a bunch of sprites that we can access for later. Dictionaries and Arrays in Godot
#do not take sprites/textures as dat. 
var _drySprites = [
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_haka.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_tallow.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_bombom.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_shnoot.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_poff.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_aile.png"),
]


var _collision: MoveableObject

var _lidDefault: Vector2
var _lidTarget: Vector2

export var _moveSpeed: int

func _ready():
	_collision = get_node("MoveableObject")
	_collision._lerpSpeed = _moveSpeed
	
	_calculateLidPos()
	
	
func _physics_process(delta):
	_calculateLidPos()
	print(_lidTarget)
	pass


func _calculateLidPos():
	_lidDefault = $MoveableObject.global_position
	_lidTarget = Vector2 (
		$MoveableObject.global_position.x + 30,
		$MoveableObject.global_position.y - 30
	)


func _on_MoveableObject_input_event(viewport, event, shape_idx):
	if Input.is_action_pressed("click"):
		_collision.selected = true


func _on_Lid_mouse_entered():
	var tween := create_tween()
	var _isPlaying = false
	
	if tween.is_running():
		_isPlaying = true
	else:
		_isPlaying = false
	
	if !_isPlaying:
		tween.tween_property($MoveableObject/Lid, "global_position", _lidTarget, 1) 


func _on_Lid_mouse_exited():
	var resetTween = create_tween()
	resetTween.tween_property($MoveableObject/Lid, "global_position", _lidDefault, 1)
	pass # Replace with function body.
