extends Area2D

var selected: bool = false

func _on_Area2D_input_event(viewport, event, shape_idx):
	if Input.is_action_pressed("click"):
		selected = true
	
func _input(event):
	#if our input relates at all to the mouse buttons:
	if event is InputEventMouseButton:
	#We check if the button index is our left mouse button and if the event is not pressed.
		if event.button_index == BUTTON_LEFT and not event.pressed:
	#Then we unselect the object. 
			selected = false

func  _physics_process(delta):
	#We can write this two ways(bools): 
	#if variable == true:
	#or 
	#if variable:
	
	#if we're grabbing the object
	if selected:
		#the global position of the object is moved to the global mouse position at a rate of our frame rate.
		global_position = lerp(global_position, get_global_mouse_position(), 25 * delta)
		#Remember if we're lerping we're giving it a start position, an end position and how many states in between (time between)
