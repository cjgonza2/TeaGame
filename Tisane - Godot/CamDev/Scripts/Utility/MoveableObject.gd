class_name MoveableObject

extends Area2D

var selected: bool = false

var _lerpSpeed: int

#NOTE: we can have basic stuff comprised in these composition scripts.
	#However, When it comes to things like signals we have to plug them in on our own.
	#I think?

func _input(event):
	#if our input relates at all to the mouse buttons:
	if event is InputEventMouseButton:
	#We check if the button index is our left mouse button and if the event is not pressed.
		if event.button_index == MOUSE_BUTTON_LEFT and not event.pressed:
	#Then we unselect the object. 
			selected = false

func  _physics_process(delta):
	#if we're grabbing the object
	if selected:
		#the global position of the object is moved to the global mouse position at a rate of our frame rate.
		global_position = lerp(global_position, get_global_mouse_position(), _lerpSpeed * delta)
		#Remember if we're lerping we're giving it a start position, an end position and how many states in between (time between)
