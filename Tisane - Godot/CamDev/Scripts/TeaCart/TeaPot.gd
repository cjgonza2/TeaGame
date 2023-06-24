extends Node2D

const _default = preload("res://CamDev/Sprites/PlayerTools/TeaPot/teapot_body_empty.png")

#I wish I was joking other than creating a sprite sheet I think this is the only way to cache
#a bunch of sprites that we can access for later. Dictionaries and Arrays in Godot
#do not take sprites/textures as data types. 
var _drySprites = [
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_haka.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_tallow.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_bombom.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_shnoot.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_poff.png"),
	preload("res://CamDev/Sprites/PlayerTools/TeaPot/DrySprites/teapot_individual_aile.png"),
]

var _collision: MoveableObject

export var _moveSpeed: int

func _ready():
	_collision = get_node("MoveableObject")
	_collision._lerpSpeed = _moveSpeed

func _on_MoveableObject_input_event(viewport, event, shape_idx):
	if Input.is_action_pressed("click"):
		_collision.selected = true
