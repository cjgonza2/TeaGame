class_name TeaClumpContainer

extends Node2D

const _highLight = preload("res://CamDev/Sprites/Ingredients/TeaContainers/container_highlight.png")

###NOTE: Okay. I'm going to try and explain what this is doing as best as possible.
		#in Godot, at least in Godot 3.5.2, you can't directly export certain data types,
		#such as KinematicBody2ds for example (what I was trying to do here.)
		
		#@"" is just another way of saying NodePath
@export var _clump_path := @""; # So We have to have a public reference to the node path. 
@onready var _clump := get_node(_clump_path) as Node2D #Then we set our clump variable equal to 
													#to the node that we grabbed from our node path.

@export var _containerSpr: Texture2D #public sprite reference to whatever container we are
@onready var _sprite = get_node("Area2D/ContainerSprite") #this just gets our sprite node
@onready var _highLightSprite = get_node("Area2D/HighLightSprite") #reference to our highlight node when we need to highlight our sprite.


func _ready():
	_sprite.texture = _containerSpr
	pass


func _on_Area2D_mouse_entered():
	_highLightSprite.texture = _highLight


func _on_Area2D_mouse_exited():
	_highLightSprite.texture = null


func _on_Area2D_input_event(viewport, event, shape_idx):
	if Input.is_action_just_pressed("click"):
		_clump.global_position = get_global_mouse_position()
		_clump._selected = true
		print(_clump._ingName)
