class_name TeaClump

extends CharacterBody2D

#Sprite variables
@export var _clumpSpr: Texture2D
@onready var _sprite = get_node("Sprite2D")

@export var _ingName: String
signal _sendName(_ingName) #the signal we emmit to send the tea clump's name to the steeper.

var _selected: bool = false
var _offScreen: bool = false
var _lidOpen: bool = false

#Physics variables
@export var _gravityRate:int #the rate at which the tea clump falls
var _gravity: Vector2 = Vector2() #holds the gravity rate so that we can add it to the velocity
var _velocity: Vector2 = Vector2() #Tea clumps downward velocity

@onready var _restPos: Vector2 = global_position

###NOTE: KinematicBody2Ds (at least in 3.5.2) can't really detect inputs,
		#even if they have a collision shape. The easiest way around this is for
		#you to give your kinematicbody a area2d child so that it can detect any
		#input/collisions.
		
func _ready():
	print(_restPos)
	_sprite.texture = _clumpSpr #sets our texture to whichever clump we give it.
	_gravity = Vector2(0, _gravityRate) #sets our gravity rate. we can't do this when we declare the variable
										#becasue at the moment of decleration the _gravityrate variable has no
										#value. 


func _input(event):
	if event is InputEventMouseButton:
		if event.button_index == MOUSE_BUTTON_LEFT and not event.pressed:
			_selected = false

func _physics_process(delta):	
	#print(_velocity)
	if _selected: 
		global_position = lerp(global_position, get_global_mouse_position(), 25 * delta)
		_velocity = Vector2.ZERO #we reset the velocity once we have it selected.
		return
		
	if _offScreen: return
	
	_velocity += _gravity * delta
	global_position.y += _velocity.y


func _on_Area2D_input_event(viewport, event, shape_idx):
	if Input.is_action_just_pressed("click"):
		_selected = true

#Visibility Notifier node is awesome becasue it tells us if an object is off screen
#is that not gas?

func _on_VisibilityNotifier2D_screen_exited():
	_velocity = Vector2.ZERO	#once we're offscreen we turn off the 
	_offScreen = true
	


func _on_VisibilityNotifier2D_screen_entered():
	_offScreen = false


func _on_Area2D_area_entered(area):
	if area.get_name() == "MoveableObject":
		if _lidOpen:
			emit_signal("_sendName", _ingName)
			global_position = _restPos
			_velocity = Vector2.ZERO


func _on_TeaPot__lidIsOpen():
	_lidOpen = true
