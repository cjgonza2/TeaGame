
extends Node2D

signal _lidIsOpen

###CONSTANT VARs
const _default = preload("res://CamDev/Sprites/PlayerTools/TeaPot/teapot_body_empty.png")
const _hightLightSpr = preload("res://CamDev/Sprites/PlayerTools/TeaPot/teapot_highlight.png")
const _lidHighLightSpr = preload("res://CamDev/Sprites/PlayerTools/TeaPot/lid_highlight.png")

#the sprite nodes
onready var _bodySpr = get_node("MoveableObject/TeaPot_Body_Sprite")
onready var _highLight = get_node("MoveableObject/TeaPot_HighLight_Sprite")
onready var _lidHighlight = get_node("MoveableObject/Lid/Lid_HighLight_Sprite")

#NOTE: dictionaries sort keys alphabetically. 
#sprite dictionary - for now holds all of our dry tea pot sprites
#since we can't make a dictionary of sprites, we can just get a string of the node path,
#then we can just load the string as a sprite when we need it. 
export var _spriteDir = {}

###############################################################

onready var _collision: MoveableObject = get_node("MoveableObject")  #refernce to moveable obj node
												#this is how we'll talk to the moveable obj script

###############################################################

var _lidDefault: Vector2	#default pos of the tea lid.
var _lidTarget: Vector2		#target tween pos based on the tea pot's body. 

export var _ingredientOne: String
export var _ingredientTwo: String

var _filled: bool #if the pot is filled with water.
var _lidOpen: bool
var _base: bool
var _infusion: bool

###NOTE: honestly I don't really understand how this is working with a tween node
		#vs without a tween node. this seems to be the only thing that doesn't 
		#consistently break it. 
onready var _lidObj = get_node("MoveableObject/Lid")
onready var _openLid = get_node("MoveableObject/Lid/Tween")
onready var _closeLid = get_node("MoveableObject/Lid/Tween")

export var _moveSpeed: int #move speed of the object that determines lerp speed.


func _ready():
	_collision._lerpSpeed = _moveSpeed #we set our lerpspeed.
	#print("this is: ", _spriteDir.keys()[0])
	print(_spriteDir["dryHaka"])
	pass

func _physics_process(delta):
	
	_calculateLidPos()	#Here so we always have a consistent update to our lid pos.
	
	if _lidObj.global_position == _lidDefault:
		print("lid in default position")
	else:
		print("lid is not in default position")
	
	if _collision.selected: #if we're selected we disable the highlight sprites.
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

func _setIngredient(name: String):
	print("called set ingredient ", name)
	if !_base and !_infusion:
		if name == "haka" or "tallow" or "bombom":
			_ingredientOne = name
			_base = true
		elif name == "shnoot" or "aile" or "poffberry":
			_ingredientOne = name
			_infusion = true
		else:
			_ingredientTwo = name

	_changeSprite(_ingredientOne)

	if _ingredientOne and _ingredientTwo != null:
		_checkBlend()
		pass


func _changeSprite(clumpName):
	print("clumpname ", clumpName)
	if _base and !_infusion or !_base and _infusion:
		match(_ingredientOne):
			"haka":
				if !_filled:
					_bodySpr.texture = load(_spriteDir["dryHaka"])
				else:
					pass
			"tallow":
				_bodySpr.texture = load(_spriteDir["dryTallow"])
			"bombom":
				_bodySpr.texture = load(_spriteDir["dryBomBom"])
			"poffBerry":
				_bodySpr.texture = load(_spriteDir["dryPoffBerry"])
			"shnoot":
				_bodySpr.texture = load(_spriteDir["dryShnoot"])
			"aile":
				_bodySpr.texture = load(_spriteDir["dryAile"])
	pass

func _checkBlend():
	pass
###############################################################


func _on_MoveableObject_input_event(viewport, event, shape_idx):
	if Input.is_action_just_pressed("click"):
		_collision.selected = true #tells the moveable obj script that we're selected
	pass


func _on_Lid_mouse_entered():
	###NOTE: this function is only temporary, just needed to understand the logic of
		#getting the lid to move. will probably use area_entered signal
		
	if _collision.selected: return #here to prevent the mouse accidentally entering
									#the lid's collision shape and causing the tween to break. 
	emit_signal("_lidIsOpen")
	
	_openLid = get_tree().create_tween() #again for some reason THIS way doesn't break the tween. 
										#if we don't have the (get_tree) it throws an error that 
										#there is no tree. 


	_openLid.tween_property(	#tweens the lid to our target position.
		$MoveableObject/Lid, "global_position", _lidTarget, 0.3)
	_openLid.parallel().tween_property(		#creates a tween parralel to the first one.
		$MoveableObject/Lid, "rotation_degrees", 35, 0.3)


func _on_Lid_mouse_exited():
	if _collision.selected:return
	
	_closeLid = get_tree().create_tween()
	
	_closeLid.tween_property(
		$MoveableObject/Lid, "global_position", _lidDefault, 0.3)
	_closeLid.parallel().tween_property(
		$MoveableObject/Lid, "rotation_degrees", 0, 0.3)


func _on_MoveableObject_mouse_entered():
	_highLight.texture = _hightLightSpr
	_lidHighlight.texture = _lidHighLightSpr


func _on_MoveableObject_mouse_exited():
	_highLight.texture = null
	_lidHighlight.texture = null


func _on_TallowClump__sendName(_ingName):
	_setIngredient(_ingName)


func _on_HakaClump__sendName(_ingName):
	_setIngredient(_ingName)


func _on_BomBomClump__sendName(_ingName):
	_setIngredient(_ingName)


func _on_AileClump__sendName(_ingName):
	_setIngredient(_ingName)


func _on_PoffBerryClump__sendName(_ingName):
	_setIngredient(_ingName)


func _on_ShnootClump__sendName(_ingName):
	_setIngredient(_ingName)
