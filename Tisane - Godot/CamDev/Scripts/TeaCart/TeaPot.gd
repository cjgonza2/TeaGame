extends Node2D

###CONSTANT VARs
const _default = preload("res://CamDev/Sprites/PlayerTools/TeaPot/teapot_body_empty.png")
const _hightLightSpr = preload("res://CamDev/Sprites/PlayerTools/TeaPot/teapot_highlight.png")
const _lidHighLightSpr = preload("res://CamDev/Sprites/PlayerTools/TeaPot/lid_highlight.png")

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

onready var _collision: MoveableObject = get_node("MoveableObject")  #refernce to moveable obj node
												#this is how we'll talk to the moveable obj script
onready var _highLight = get_node("MoveableObject/TeaPot_HighLight_Sprite")
onready var _lidHighlight = get_node("MoveableObject/Lid/Lid_HighLight_Sprite")

var _lidDefault: Vector2	#default pos of the tea lid.
var _lidTarget: Vector2		#target tween pos based on the tea pot's body. 


###NOTE: honestly I don't really understand how this is working with a tween node
		#vs without a tween node. this seems to be the only thing that doesn't 
		#consistently break it. 
onready var _openLid = get_node("MoveableObject/Lid/Tween")
onready var _closeLid = get_node("MoveableObject/Lid/Tween")

export var _moveSpeed: int #move speed of the object that determines lerp speed.


func _ready():
	_collision._lerpSpeed = _moveSpeed #we set our lerpspeed.
	pass
	
	
func _physics_process(delta):
	_calculateLidPos()	#Here so we always have a consistent update to our lid pos.
	
	if _collision.selected:
		if _highLight.texture and _lidHighlight.texture != null:
			_highLight.texture = null
			_lidHighlight.texture = null
	pass


func _calculateLidPos(): 
	_lidDefault = $MoveableObject.global_position #just equals our moveable obj transform.
	_lidTarget = Vector2 (
		$MoveableObject.global_position.x + 30,
		$MoveableObject.global_position.y - 30
	)	#This calculates based on our teapot's position, currently using an arbitrary value.


func _on_MoveableObject_input_event(viewport, event, shape_idx):
	if Input.is_action_pressed("click"):
		_collision.selected = true #tells the moveable obj script that we're selected
	

	


func _on_Lid_mouse_entered():
	###NOTE: this function is only temporary, just needed to understand the logic of
		#getting the lid to move. will probably use area_entered signal
		
	if _collision.selected: return #here to prevent the mouse accidentally entering
									#the lid's collision shape and causing the tween to break. 

		
	_openLid = get_tree().create_tween() #again for some reason THIS way doesn't break the tween. 
										#if we don't have the (get_tree) it throws an error that 
										#there is no tree. 

	
	_openLid.tween_property(	#tweens the lid to our target position.
		$MoveableObject/Lid, "global_position", _lidTarget, 0.3)
	_openLid.parallel().tween_property(		#creates a tween parralel to the first one.
		$MoveableObject/Lid, "rotation_degrees", 35, 0.3)
	pass
		

func _on_Lid_mouse_exited():
	if _collision.selected:return
	
	_closeLid = get_tree().create_tween()
	
	_closeLid.tween_property(
		$MoveableObject/Lid, "global_position", _lidDefault, 0.3)
	_closeLid.parallel().tween_property(
		$MoveableObject/Lid, "rotation_degrees", 0, 0.3)
	pass


func _on_MoveableObject_mouse_entered():
	_highLight.texture = _hightLightSpr
	_lidHighlight.texture = _lidHighLightSpr
	pass
	

func _on_MoveableObject_mouse_exited():
	_highLight.texture = null
	_lidHighlight.texture = null
