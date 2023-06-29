extends Node2D

const _default = preload("res://CamDev/Sprites/PlayerTools/TeaCup/teacup_pixel.png")
const _highLightSpr = preload("res://CamDev/Sprites/PlayerTools/TeaCup/teacup_highlight.png")

onready var _baseSpr = get_node("MoveableObject/BaseSprite")
onready var _highlight = get_node("MoveableObject/Highlight")


onready var _collision: MoveableObject = get_node("MoveableObject")  #refernce to moveable obj node
												#this is how we'll talk to the moveable obj script
export var _moveSpeed: int #move speed of the object that determines lerp speed.


func _ready():
	_collision._lerpSpeed = _moveSpeed #we set our lerpspeed.
	pass


func _process(delta):
	if _collision.selected:
		if _highlight.texture != null:
			_highlight.texture = null

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_MoveableObject_input_event(viewport, event, shape_idx):
	if Input.is_action_pressed("click"):
		_collision.selected = true #tells the moveable obj script that we're selected
	pass


func _on_MoveableObject_mouse_entered():
	_highlight.texture = _highLightSpr
	pass


func _on_MoveableObject_mouse_exited():
	_highlight.texture = null
	pass # Replace with function body.
